namespace Spectrum.Content.Appointments.Providers
{
    using ContentModels;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Services;
    using Translators;
    using Umbraco.Web;
    using ViewModels;

    public class GoogleCalendarProvider : ICalendarProvider
    {
        /// <summary>
        /// The google calendar services.
        /// </summary>
        private readonly IGoogleCalendarServices googleCalendarServices;

        /// <summary>
        /// The google event translator.
        /// </summary>
        private readonly IGoogleEventTranslator googleEventTranslator;

        /// <summary>
        /// The appointments provider.
        /// </summary>
        private readonly IAppointmentsProvider appointmentsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarProvider" /> class.
        /// </summary>
        /// <param name="googleCalendarServices">The google calendar services.</param>
        /// <param name="googleEventTranslator">The google event translator.</param>
        /// <param name="appointmentsProvider">The appointments provider.</param>
        public GoogleCalendarProvider(
            IGoogleCalendarServices googleCalendarServices,
            IGoogleEventTranslator googleEventTranslator,
            IAppointmentsProvider appointmentsProvider)
        {
            this.googleCalendarServices = googleCalendarServices;
            this.googleEventTranslator = googleEventTranslator;
            this.appointmentsProvider = appointmentsProvider;
        }

        /// <summary>
        /// Gets the calendar URL.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public string GetCalendarUrl(UmbracoContext umbracoContext)
        {
            AppointmentsModel model = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            return model.GoogleCalendarUrl;
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="viewModel">The view model.</param>
        public void InsertEvent(
            UmbracoContext umbracoContext,
            InsertAppointmentViewModel viewModel)
        {
            CalendarService calendarService = GetCalendarService(umbracoContext);

            Event googleEvent = googleEventTranslator.Translate(viewModel);

            Event insertedEvent = googleCalendarServices.InsertEvent(calendarService, googleEvent);
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        public void GetEvents(UmbracoContext umbracoContext)
        {
            CalendarService calendarService = GetCalendarService(umbracoContext);

            googleCalendarServices.GetEvents(calendarService);
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="eventId">The event identifier.</param>
        public void GetEvent(
            UmbracoContext umbracoContext, 
            string eventId)
        {
            CalendarService calendarService = GetCalendarService(umbracoContext);

            Event calendarEvent = googleCalendarServices.GetEvent(calendarService, eventId);
        }

        /// <summary>
        /// Gets the calendar service.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        internal CalendarService GetCalendarService(UmbracoContext umbracoContext)
        {
            AppointmentsModel model = this.appointmentsProvider.GetAppointmentsModel(umbracoContext); 

            UserCredential userCredential = googleCalendarServices.GetCredentials(
                                                model.GoogleClientId,
                                                model.GoogleClientSecret,
                                                model.RedirectUrl);

            return googleCalendarServices.GetCalendarService(userCredential, model.GoogleCalendarName);
        }
    }
}