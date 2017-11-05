using System.Globalization;

namespace Spectrum.Content.Appointments.Managers
{
    using Application.Services;
    using Autofac.Events;
    using Content.Models;
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

        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="createdUser">The created user.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public string InsertAppointment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            InsertAppointmentViewModel viewModel,
            string createdUser)
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

            CustomerModel customerModel = appointmentsProvider.GetCustomerModel(umbracoContext);

            AppointmentModel appointmentModel = insertAppointmentTranslator.Translate(viewModel);
            appointmentModel.CustomerId = customerModel.Id;
            appointmentModel.CreatedUser = createdUser;
            appointmentModel.LastedUpdatedUser = createdUser;
            
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

                Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    {"AppointmentId", appointmentId.ToString()},
                    {"AppointmentStartTime", viewModel.StartTime.ToLongTimeString()},
                    {"AppointmentDuration", viewModel.Duration.ToString()},
                    {"AppointmentLocation", viewModel.Location},
                    {"AppointmentDescription", viewModel.Description}
                };

                //// try and send the email
                if (string.IsNullOrEmpty(settingsModel.IcalEmailAddress) == false)
                {
                    mailProvider.SendEmail(
                        umbracoContext, 
                        settingsModel.IcalCreateEmailTemplate, 
                        settingsModel.IcalEmailAddress, 
                        attachment,
                        dictionary);

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
                            attachment,
                            dictionary);
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

                CustomerModel customerModel = appointmentsProvider.GetCustomerModel(umbracoContext);
                int customerId = customerModel.Id;

                AppointmentModel model = databaseProvider.GetAppointment(Convert.ToInt32(id), customerId);

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

            AppointmentSettingsModel appointmentSettingsModel = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            int customerId = GetCustomerId(umbracoContext);
            
            if (appointmentSettingsModel.DatabaseIntegration)
            {
                IEnumerable<AppointmentModel> models = databaseProvider.GetAppointments(
                                                            dateRangeStart,
                                                            dateRangeEnd,
                                                            customerId);

                List<AppointmentViewModel> viewModels = new List<AppointmentViewModel>();

                string paymentsPage = appointmentSettingsModel.PaymentsPage;

                foreach (AppointmentModel appointmentModel in models)
                {
                    viewModels.Add(appointmentTranslator.Translate(paymentsPage, appointmentModel));
                }

                return viewModels;
            }

            return new List<AppointmentViewModel>();
        }

        /// <summary>
        /// Gets the boot grid appointments.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public BootGridViewModel<AppointmentViewModel> GetBootGridAppointments(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext, 
            DateTime dateRangeStart,
            DateTime dateRangeEnd)
        {
            IEnumerable<AppointmentViewModel> viewModels = GetAppointments(
                umbracoContext,
                dateRangeStart,
                dateRangeEnd);

            return appointmentsBootGridTranslator.Translate(
                viewModels.ToList(), 
                current, 
                rowCount, 
                searchPhrase,
                sortItems);
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        public string DeleteAppointment(
            UmbracoContext umbracoContext, 
            string appointmentId)
        {
            AppointmentSettingsModel appointmentSettingsModel = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            string id = encryptionService.DecryptString(appointmentId);

            int appId = Convert.ToInt32(id);
            
            int customerId = GetCustomerId(umbracoContext);

            AppointmentModel appointmentModel = databaseProvider.GetAppointment(appId, customerId);

            if (appointmentSettingsModel.DatabaseIntegration)
            {
                appointmentModel.Status = (int)AppointmentStatus.Deleted;

                databaseProvider.UpdateAppointment(appointmentModel);
            }

            if (appointmentSettingsModel.IcalIntegration &&
                string.IsNullOrEmpty(appointmentSettingsModel.IcalDeleteEmailTemplate) == false)
            {
                //// now send the delete ical email

                ICalAppointmentModel iCalModel = databaseProvider.GetIcalAppointment(appId);

                ICalAppointmentModel icalAppointmentModel = iCalendarService.GetICalAppoinment(
                                                            appointmentModel, 
                                                            iCalModel.Guid, 
                                                            iCalModel.Sequence + 1);

                Attachment attachment = Attachment.CreateAttachmentFromString(
                    icalAppointmentModel.SerializedString, 
                    icalAppointmentModel.ContentType);

                Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    {"AppointmentId", appointmentId}
                };

                mailProvider.SendEmail(
                    umbracoContext,
                    appointmentSettingsModel.IcalDeleteEmailTemplate,
                    appointmentSettingsModel.IcalEmailAddress,
                    attachment);

                if (appointmentSettingsModel.IcalSendToAttendees)
                {
                    if (appointmentModel.Attendees != null)
                    {
                        foreach (AppointmentAttendeeModel attendeeModel in appointmentModel.Attendees)
                        {
                            mailProvider.SendEmail(
                                umbracoContext,
                                appointmentSettingsModel.IcalDeleteEmailTemplate,
                                attendeeModel.EmailAddress,
                                attachment,
                                dictionary);
                        }
                    }
                }
            }

            if (appointmentSettingsModel.GoogleCalendarIntegration)
            {
            }

            //// TODO : this needs changing!!
            return "/customer/dashboard";
        }

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="lastUpdatedUser">The last updated user.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public string UpdateAppointment(
            UmbracoContext umbracoContext,
            AppointmentViewModel viewModel,
            string lastUpdatedUser)
        {
            loggingService.Info(GetType(), "Start");

            AppointmentSettingsModel appointmentSettingsModel = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            AppointmentModel originalModel = databaseProvider.GetAppointment(Convert.ToInt32(viewModel.Id), GetCustomerId(umbracoContext));

            AppointmentModel appointmentModel = appointmentTranslator.Translate(originalModel, viewModel);
            appointmentModel.LastedUpdatedUser = lastUpdatedUser;

            if (appointmentSettingsModel.DatabaseIntegration)
            {
                loggingService.Info(GetType(), "Database Integration");

                databaseProvider.UpdateAppointment(appointmentModel);
            }

            if (appointmentSettingsModel.GoogleCalendarIntegration)
            {
                loggingService.Info(GetType(), "Google Calendar Integration");
            }

            if (appointmentSettingsModel.IcalIntegration &&
                string.IsNullOrEmpty(appointmentSettingsModel.IcalUpdateEmailTemplate) == false)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    {"AppointmentId", viewModel.Id.ToString()},
                    {"AppointmentStartTime", viewModel.StartTime.ToLongTimeString()},
                    {"AppointmentDuration", viewModel.Duration.ToString(CultureInfo.InvariantCulture)},
                    {"AppointmentLocation", viewModel.Location},
                    {"AppointmentDescription", viewModel.Description}
                };

                ICalAppointmentModel iCalModel = databaseProvider.GetIcalAppointment(viewModel.Id);

                ICalAppointmentModel iCalAppointmentModel = iCalendarService.GetICalAppoinment(
                    appointmentModel,
                    iCalModel.Guid,
                    iCalModel.Sequence + 1);

                Attachment attachment = Attachment.CreateAttachmentFromString(
                    iCalAppointmentModel.SerializedString,
                    iCalAppointmentModel.ContentType);

                mailProvider.SendEmail(
                    umbracoContext,
                    appointmentSettingsModel.IcalUpdateEmailTemplate,
                    appointmentSettingsModel.IcalEmailAddress,
                    attachment,
                    dictionary);

                if (appointmentSettingsModel.IcalSendToAttendees)
                {
                    if (appointmentModel.Attendees != null)
                    {
                        foreach (AppointmentAttendeeModel attendeeModel in appointmentModel.Attendees)
                        {
                            mailProvider.SendEmail(
                                umbracoContext,
                                appointmentSettingsModel.IcalUpdateEmailTemplate,
                                attendeeModel.EmailAddress,
                                attachment,
                                dictionary);
                        }
                    }
                }
            }

            //// TODO : this needs changing!!
            return "/customer/dashboard";
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

            CustomerModel customerModel = appointmentsProvider.GetCustomerModel(paymentMadeMessage.UmbracoContext);
            int customerId = customerModel.Id;

            if (appointmentsModel.DatabaseIntegration)
            {
                UpdateAppointment(id.Value, customerId, paymentId);
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
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="paymentId">The payment identifier.</param>
        internal void UpdateAppointment(
            int id, 
            int customerId,
            string paymentId)
        {
            AppointmentModel model = databaseProvider.GetAppointment(id, customerId);

            if (model != null)
            {
                loggingService.Info(GetType(), "Appointment Found");

                model.PaymentId = paymentId;

                databaseProvider.UpdateAppointment(model);

                loggingService.Info(GetType(), "Appointment updated with PaymentId=" + paymentId);
            }
        }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        internal int GetCustomerId(UmbracoContext context)
        {
            CustomerModel customerModel = appointmentsProvider.GetCustomerModel(context);
            return customerModel.Id;
        }
    }
}