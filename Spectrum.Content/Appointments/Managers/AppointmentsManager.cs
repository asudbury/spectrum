using Spectrum.Content.Appointments.Services;

namespace Spectrum.Content.Appointments.Managers
{
    using Application.Services;
    using Autofac.Events;
    using Content.Services;
    using ContentModels;
    using Messages;
    using Models;
    using Providers;
    using System;
    using System.Collections.Generic;
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
        public AppointmentsManager(
            ILoggingService loggingService,
            IAppointmentsProvider appointmentsProvider,
            IInsertAppointmentTranslator insertappointmentTranslator,
            IDatabaseProvider databaseProvider,
            IICalendarService iCalendarService,
            IEventPublisher eventPublisher,
            ICookieService cookieService,
            IAppointmentTranslator appointmentTranslator,
            IEncryptionService encryptionService)
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
        }

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

            AppointmentSettingsModel model = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            AppointmentModel appointmentModel = insertAppointmentTranslator.Translate(viewModel);

            string appointmentId = string.Empty;

            appointmentModel.CreatedUser = createdUserName;

            if (model.DatabaseIntegration)
            {
                loggingService.Info(GetType(), "Database Integration");

                appointmentId = databaseProvider.InsertAppointment(appointmentModel);

                if (string.IsNullOrEmpty(appointmentId) == false)
                {
                    cookieService.SetValue(AppointmentConstants.LastAppointmentIdCookie, appointmentId);
                    eventPublisher.Publish(new AppointmentMadeMessage(appointmentId));
                }

                processed = true;
            }

            if (model.GoogleCalendarIntegration)
            {
                loggingService.Info(GetType(), "Google Calendar Integration");
                processed = true;    
            }

            if (model.iCalIntegration)
            {
                loggingService.Info(GetType(), "iCal Integration");

                ICalAppointmentModel iCalModel = iCalendarService.GetICalAppoinment(appointmentModel);

                processed = true;
            }

            if (processed == false)
            {
                loggingService.Info(GetType(), "No integration setting set to be processed");
                return pageModel.ErrorPageUrl;
            }

            loggingService.Info(GetType(), "End");

            return pageModel.NextPageUrl;
        }

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
            AppointmentSettingsModel appointmentsModel = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            if (appointmentsModel.DatabaseIntegration)
            {
                string id = encryptionService.DecryptString(appointmentId);

                AppointmentModel model = databaseProvider.GetAppointment(Convert.ToInt32(id));

                model.Status = (int)AppointmentStatus.Deleted;

                databaseProvider.UpdateAppointment(model);
            }

            return true;
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