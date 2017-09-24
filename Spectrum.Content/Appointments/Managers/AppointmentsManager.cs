namespace Spectrum.Content.Appointments.Managers
{
    using Application.Services;
    using Autofac.Events;
    using Content.Services;
    using ContentModels;
    using Mail.Providers;
    using Messages;
    using Models;
    using Providers;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using Translators;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class AppointmentsManager : IAppointmentsManager, IHandleEvent<PaymentMadeMessage>
    {
        /// <summary>
        /// The logging service.
        /// </summary>
        private readonly ILoggingService loggingService;

        /// <summary>
        /// The appointments provider.
        /// </summary>
        private readonly IAppointmentsProvider appointmentsProvider;

        /// <summary>
        /// The insert appointment translator.
        /// </summary>
        private readonly IInsertAppointmentTranslator insertAppointmentTranslator;

        /// <summary>
        /// The database provider.
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        /// <summary>
        /// The icalendar service.
        /// </summary>
        private readonly IICalendarService iCalendarService;

        /// <summary>
        /// The event publisher.
        /// </summary>
        private readonly IEventPublisher eventPublisher;

        /// <summary>
        /// The cookie service.
        /// </summary>
        private readonly ICookieService cookieService;

        /// <summary>
        /// The appointment translator.
        /// </summary>
        private readonly IAppointmentTranslator appointmentTranslator;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// The mail provider.
        /// </summary>
        private readonly IMailProvider mailProvider;

        /// <summary>
        /// The appointments boot grid translator.
        /// </summary>
        private readonly IAppointmentsBootGridTranslator appointmentsBootGridTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsProvider">The appointments provider.</param>
        /// <param name="insertappointmentTranslator">The insertappointment translator.</param>
        /// <param name="databaseProvider">The database provider.</param>
        /// <param name="iCalendarService">The i calendar service.</param>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="cookieService">The cookie service.</param>
        /// <param name="appointmentTranslator">The appointment translator.</param>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="mailProvider">The email provider.</param>
        /// <param name="appointmentsBootGridTranslator">The appointments boot grid translator.</param>
        public AppointmentsManager(
            ILoggingService loggingService,
            IAppointmentsProvider appointmentsProvider,
            IInsertAppointmentTranslator insertappointmentTranslator,
            IDatabaseProvider databaseProvider,
            IICalendarService iCalendarService,
            IEventPublisher eventPublisher,
            ICookieService cookieService,
            IAppointmentTranslator appointmentTranslator,
            IEncryptionService encryptionService,
            IMailProvider mailProvider,
            IAppointmentsBootGridTranslator appointmentsBootGridTranslator)
        {
            this.loggingService = loggingService;
            this.appointmentsProvider = appointmentsProvider;
            this.insertAppointmentTranslator = insertappointmentTranslator;
            this.databaseProvider = databaseProvider;
            this.iCalendarService = iCalendarService;
            this.eventPublisher = eventPublisher;
            this.cookieService = cookieService;
            this.appointmentTranslator = appointmentTranslator;
            this.encryptionService = encryptionService;
            this.mailProvider = mailProvider;
            this.appointmentsBootGridTranslator = appointmentsBootGridTranslator;
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="createdUserName">Name of the created user.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public string InsertAppointment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            string createdUserName,
            InsertAppointmentViewModel viewModel)
        {
            loggingService.Info(GetType(), "Start");

            bool processed = false;

            PageModel pageModel = new PageModel(publishedContent);

            if (string.IsNullOrEmpty(pageModel.NextPageUrl))
            {
                loggingService.Info(GetType(), "Next Page Url not set");
            }

            if (string.IsNullOrEmpty(pageModel.ErrorPageUrl))
            {
                loggingService.Info(GetType(), "Error Page Url not set");
            }

            AppointmentSettingsModel settingsModel = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            AppointmentModel appointmentModel = insertAppointmentTranslator.Translate(viewModel);

            appointmentModel.CreatedUser = createdUserName;

            int appointmentId = 0;

            if (settingsModel.DatabaseIntegration)
            {
                loggingService.Info(GetType(), "Database Integration");

                appointmentId = databaseProvider.InsertAppointment(appointmentModel);

                if (appointmentId > 0)
                {
                    cookieService.SetValue(AppointmentConstants.LastAppointmentIdCookie, appointmentId);
                    eventPublisher.Publish(new AppointmentMadeMessage(appointmentId));
                }

                processed = true;
            }

            if (settingsModel.GoogleCalendarIntegration)
            {
                loggingService.Info(GetType(), "Google Calendar Integration");
                processed = true;    
            }

            if (settingsModel.IcalIntegration)
            {
                loggingService.Info(GetType(), "iCal Integration");

                ICalAppointmentModel iCalModel = iCalendarService.GetICalAppoinment(appointmentModel);

                Attachment attachment = Attachment.CreateAttachmentFromString(iCalModel.SerializedString, iCalModel.ContentType);

                //// try and send the email
                if (string.IsNullOrEmpty(settingsModel.IcalEmailAddress) == false)
                {
                    mailProvider.SendEmail(
                        umbracoContext, 
                        settingsModel.IcalCreateEmailTemplate, 
                        settingsModel.IcalEmailAddress, 
                        attachment);

                    //// now update the database 
                    if (appointmentId > 0)
                    {
                        iCalModel.AppointmentId = appointmentId;

                        databaseProvider.InsertIcalAppointment(iCalModel);
                    }
                }

                if (settingsModel.IcalSendToAttendees)
                {
                    foreach (AppointmentAttendeeModel attendeeModel in appointmentModel.Attendees)
                    {
                        mailProvider.SendEmail(
                            umbracoContext,
                            settingsModel.IcalCreateEmailTemplate,
                            attendeeModel.EmailAddress,
                            attachment);
                    }
                }

                processed = true;
            }

            if (processed == false)
            {
                loggingService.Info(GetType(), "No integration setting set to be processed");
                return pageModel.ErrorPageUrl;
            }

            cookieService.SetValue(AppointmentConstants.LastAppointmentDuration, viewModel.Duration);

            loggingService.Info(GetType(), "End");

            return pageModel.NextPageUrl;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        public AppointmentViewModel GetAppointment(
            UmbracoContext umbracoContext,
            string appointmentId)
        {
            loggingService.Info(GetType());

            AppointmentSettingsModel settingsModel = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            if (settingsModel.DatabaseIntegration)
            {
                string id = encryptionService.DecryptString(appointmentId);

                AppointmentModel model = databaseProvider.GetAppointment(Convert.ToInt32(id));

                return appointmentTranslator.Translate(settingsModel.PaymentsPage, model);
            }

            return new AppointmentViewModel();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the appointments.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        public IEnumerable<AppointmentViewModel> GetAppointments(
           UmbracoContext umbracoContext,
            DateTime dateRangeStart,
            DateTime dateRangeEnd)
        {
            loggingService.Info(GetType());

            AppointmentSettingsModel model = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            if (model.DatabaseIntegration)
            {
                IEnumerable<AppointmentModel> models = databaseProvider.GetAppointments(
                                                            dateRangeStart,
                                                            dateRangeEnd);

                List<AppointmentViewModel> viewModels = new List<AppointmentViewModel>();

                string paymentsPage = model.PaymentsPage;

                foreach (AppointmentModel appointmentModel in models)
                {
                    viewModels.Add(appointmentTranslator.Translate(paymentsPage, appointmentModel));
                }

                return viewModels;
            }

            return new List<AppointmentViewModel>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the boot grid appointments.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        public BootGridViewModel<AppointmentViewModel> GetBootGridAppointments(
            int current,
            int rowCount,
            string searchPhrase,
            UmbracoContext umbracoContext, 
            DateTime dateRangeStart,
            DateTime dateRangeEnd)
        {
            IEnumerable<AppointmentViewModel> viewModels = GetAppointments(
                umbracoContext,
                dateRangeStart,
                dateRangeEnd);

            return appointmentsBootGridTranslator.Translate(viewModels.ToList(), current, rowCount, searchPhrase);
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        public bool DeleteAppointment(
            UmbracoContext umbracoContext, 
            string appointmentId)
        {
            AppointmentSettingsModel settingsModel = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            string id = encryptionService.DecryptString(appointmentId);

            int appId = Convert.ToInt32(id);

            AppointmentModel model = databaseProvider.GetAppointment(appId);

            if (settingsModel.DatabaseIntegration)
            {
                model.Status = (int)AppointmentStatus.Deleted;

                databaseProvider.UpdateAppointment(model);
            }

            if (settingsModel.IcalIntegration &&
                string.IsNullOrEmpty(settingsModel.IcalDeleteEmailTemplate) == false)
            {
                //// now send the delete ical email

                ICalAppointmentModel iCalModel = databaseProvider.GetIcalAppointment(appId);

                ICalAppointmentModel appointmentModel = iCalendarService.GetICalAppoinment(
                                                            model, 
                                                            iCalModel.Guid, 
                                                            iCalModel.Sequence + 1);

                Attachment attachment = Attachment.CreateAttachmentFromString(appointmentModel.SerializedString, appointmentModel.ContentType);

                mailProvider.SendEmail(
                    umbracoContext,
                    settingsModel.IcalDeleteEmailTemplate,
                    settingsModel.IcalEmailAddress,
                    attachment);
            }

            if (settingsModel.GoogleCalendarIntegration)
            {
            }

            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public string UpdateAppointment(
            UmbracoContext umbracoContext, 
            AppointmentViewModel viewModel)
        {
            loggingService.Info(GetType(), "Start");

            /*AppointmentSettingsModel settingsModel = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            AppointmentModel appointmentModel = insertAppointmentTranslator.Translate(viewModel);

            if (settingsModel.DatabaseIntegration)
            {
                loggingService.Info(GetType(), "Database Integration");

                databaseProvider.UpdateAppointment(appointmentModel);
            }

            if (settingsModel.GoogleCalendarIntegration)
            {
                loggingService.Info(GetType(), "Google Calendar Integration");
            }

            if (settingsModel.iCalIntegration)
            {
                loggingService.Info(GetType(), "iCal Integration");
            }*/

            return string.Empty;
        }

        /// <summary>
        /// Handles the specified payment made message.
        /// </summary>
        /// <param name="paymentMadeMessage">The payment made message.</param>
        public void Handle(PaymentMadeMessage paymentMadeMessage)
        {
            string paymentId = paymentMadeMessage.PaymentId;
            string autoAllocate = paymentMadeMessage.AutoAllocate;
            string appointmentId = paymentMadeMessage.AppointmentId;

            string message = "PaymentMadeMessage " + 
                             "PaymentId=" + paymentId + " " +
                             "AutoAllocate=" + autoAllocate + " " + 
                             "AppointmentId=" + appointmentId;

            loggingService.Info(GetType(), message);

            int? id = GetAppointmentId(autoAllocate, appointmentId);

            if (id.HasValue == false)
            {
                loggingService.Info(GetType(), "Payment not handled");
                return;
            }

            AppointmentSettingsModel appointmentsModel = appointmentsProvider.GetAppointmentsModel(paymentMadeMessage.UmbracoContext);

            if (appointmentsModel.DatabaseIntegration)
            {
                UpdateAppointment(id.Value, paymentId);
            }
        }

        /// <summary>
        /// Gets the appointment identifier.
        /// </summary>
        /// <param name="autoAllocate">The automatic allocate.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        internal int? GetAppointmentId(
            string autoAllocate,
            string appointmentId)
        {
            int? id = null;

            if (string.IsNullOrEmpty(appointmentId) == false)
            {
                string appId = encryptionService.DecryptString(appointmentId);

                id = Convert.ToInt32(appId);
            }

            else if (string.IsNullOrEmpty(autoAllocate) == false &&
                     autoAllocate.ToLower() == "y")
            {
                id = cookieService.GetValue<int>(AppointmentConstants.LastAppointmentIdCookie);
            }

            //// remove the cookie so it doesnt get allocated to the next payment.
            cookieService.Expire(AppointmentConstants.LastAppointmentIdCookie);

            return id;
        }

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="paymentId">The payment identifier.</param>
        internal void UpdateAppointment(
            int id, 
            string paymentId)
        {
            AppointmentModel model = databaseProvider.GetAppointment(id);

            if (model != null)
            {
                loggingService.Info(GetType(), "Appointment Found");

                model.PaymentId = paymentId;

                databaseProvider.UpdateAppointment(model);

                loggingService.Info(GetType(), "Appointment updated with PaymentId=" + paymentId);
            }
        }
    }
}