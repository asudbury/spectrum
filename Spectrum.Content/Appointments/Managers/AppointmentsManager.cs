namespace Spectrum.Content.Appointments.Managers
{
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
        /// The appointment translator.
        /// </summary>
        private readonly IAppointmentTranslator appointmentTranslator;

        /// <summary>
        /// The database provider.
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        /// <summary>
        /// The event publisher.
        /// </summary>
        private readonly IEventPublisher eventPublisher;

        /// <summary>
        /// The cookie service.
        /// </summary>
        private readonly ICookieService cookieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsProvider">The appointments provider.</param>
        /// <param name="appointmentTranslator">The appointment translator.</param>
        /// <param name="databaseProvider">The database provider.</param>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="cookieService">The cookie service.</param>
        public AppointmentsManager(
            ILoggingService loggingService,
            IAppointmentsProvider appointmentsProvider,
            IAppointmentTranslator appointmentTranslator,
            IDatabaseProvider databaseProvider,
            IEventPublisher eventPublisher,
            ICookieService cookieService)
        {
            this.loggingService = loggingService;
            this.appointmentsProvider = appointmentsProvider;
            this.appointmentTranslator = appointmentTranslator;
            this.databaseProvider = databaseProvider;
            this.eventPublisher = eventPublisher;
            this.cookieService = cookieService;
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

            AppointmentsModel model = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            AppointmentModel appointmentModel = appointmentTranslator.Translate(viewModel);

            appointmentModel.CreatedUser = createdUserName;

            if (model.DatabaseIntegration)
            {
                loggingService.Info(GetType(), "Database Integration");

                string appointmentId = databaseProvider.InsertAppointment(appointmentModel);

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
        public AppointmentModel GetAppointment(
            UmbracoContext umbracoContext,
            int appointmentId)
        {
            loggingService.Info(GetType(), string.Empty);

            AppointmentsModel model = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            if (model.DatabaseIntegration)
            {
                return databaseProvider.GetAppointment(appointmentId);
            }

            return new AppointmentModel();
        }

        /// <summary>
        /// Gets the appointments.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        public IEnumerable<AppointmentModel> GetAppointments(
            UmbracoContext umbracoContext,
            DateTime dateRangeStart, 
            DateTime dateRangeEnd)
        {
            loggingService.Info(GetType(), string.Empty);

            AppointmentsModel model = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            if (model.DatabaseIntegration)
            {
                return databaseProvider.GetAppointments(
                    dateRangeStart,
                    dateRangeEnd);
            }

            return new List<AppointmentModel>();
        }

        /// <summary>
        /// Handles the specified payment made message.
        /// </summary>
        /// <param name="paymentMadeMessage">The payment made message.</param>
        public void Handle(PaymentMadeMessage paymentMadeMessage)
        {
            string paymentId = paymentMadeMessage.PaymentId;

            loggingService.Info(GetType(), "PaymentMadeMessage PaymentId=" + paymentId);

            AppointmentsModel appointmentsModel = appointmentsProvider.GetAppointmentsModel(paymentMadeMessage.UmbracoContext);

            //// if we are supporting database integration and auto allocating of payments then proceed!
            if (appointmentsModel.DatabaseIntegration &&
                appointmentsModel.AutoAllocatePayments)
            {
                loggingService.Info(GetType(), "AutoAllocating");

                int appointmentId = cookieService.GetValue<int>(AppointmentConstants.LastAppointmentIdCookie);

                if (appointmentId > 0)
                {
                    loggingService.Info(GetType(), "AppointmentId Found from cookie");

                    AppointmentModel model = databaseProvider.GetAppointment(appointmentId);

                    if (model != null)
                    {
                        loggingService.Info(GetType(), "Appointment Found");

                        //// remove the cookie so it doesnt get allocated to the next payment.
                        cookieService.Expire(AppointmentConstants.LastAppointmentIdCookie);

                        model.PaymentId = paymentId;

                        databaseProvider.UpdateAppointment(model);

                        loggingService.Info(GetType(), "Appointment updated with PaymentId=" + paymentId);
                    }
                }
            }
        }
    }
}