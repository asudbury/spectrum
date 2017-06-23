namespace Spectrum.Content.Appointments.Providers
{
    using Content.Services;
    using ContentModels;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Services;
    using Translators;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class GoogleCalendarProvider : IGoogleCalendarProvider
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The google calendar services.
        /// </summary>
        private readonly IGoogleCalendarServices googleCalendarServices;

        /// <summary>
        /// The google event translator.
        /// </summary>
        private readonly IGoogleEventTranslator googleEventTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarProvider" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="googleCalendarServices">The google calendar services.</param>
        /// <param name="googleEventTranslator">The google event translator.</param>
        public GoogleCalendarProvider(
            ISettingsService settingsService,
            IGoogleCalendarServices googleCalendarServices,
            IGoogleEventTranslator googleEventTranslator)
        {
            this.settingsService = settingsService;
            this.googleCalendarServices = googleCalendarServices;
            this.googleEventTranslator = googleEventTranslator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarProvider"/> class.
        /// </summary>
        public GoogleCalendarProvider()
            : this(new SettingsService(),
                   new GoogleCalendarServices(),
                   new GoogleEventTranslator())
        {
        }

        /// <summary>
        /// Gets the calendar URL.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public string GetCalendarUrl(UmbracoContext umbracoContext)
        {
            IPublishedContent content = settingsService.GetAppointmentsNode(umbracoContext);

            AppointmentsModel model = new AppointmentsModel(content);

            return model.GoogleCalendarUrl;
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="viewModel">The view model.</param>
        public void InsertEvent(
            UmbracoContext umbracoContext,
            GoogleEventViewModel viewModel)
        {
            CalendarService calendarService = GetCalendarService(umbracoContext);

            Event googleEvent = googleEventTranslator.Translate(viewModel);

            googleCalendarServices.InsertEvent(calendarService, googleEvent);
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
            IPublishedContent content = settingsService.GetAppointmentsNode(umbracoContext);

            AppointmentsModel model = new AppointmentsModel(content);

            UserCredential userCredential = googleCalendarServices.GetCredentials(
                                                model.GoogleClientId,
                                                model.GoogleClientSecret);

            return googleCalendarServices.GetCalendarService(userCredential, "app");
        }
    }
}