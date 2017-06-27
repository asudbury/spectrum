namespace Spectrum.Content.Appointments.Services
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;
    using System;
    using System.Threading;
    using System.Web;

    public class GoogleCalendarServices : IGoogleCalendarServices
    {
        /// <summary>
        /// The default calendar identifier.
        /// </summary>
        private const string DefaultCalendarId = "primary";

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        /// <returns></returns>
        public UserCredential GetCredentials(
            string clientId, 
            string clientSecret,
            string redirectUrl)
        {
            WebAuthorizationBroker.RedirectUri = redirectUrl;

            return WebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                },
                new[]
                {
                    CalendarService.Scope.Calendar
                },
                "user",
                CancellationToken.None,
                new FileDataStore(HttpContext.Current.Server.MapPath("/App_Data/MyGoogleStorage"))).Result;
        }

        /// <summary>
        /// Gets the calendar service.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <returns></returns>
        public CalendarService GetCalendarService(
            UserCredential userCredential, 
            string applicationName)
        {
            return new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = userCredential,
                ApplicationName = applicationName,
            });
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="calendarService">The calendar service.</param>
        /// <param name="newEvent">The new event.</param>
        public Event InsertEvent(
            CalendarService calendarService, 
            Event newEvent)
        {
            EventsResource.InsertRequest request = calendarService.Events.Insert(newEvent, DefaultCalendarId);

            return request.Execute();
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="calendarService">The calendar service.</param>
        public Events GetEvents(CalendarService calendarService)
        {
            EventsResource.ListRequest request = calendarService.Events.List(DefaultCalendarId);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return request.Execute();
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="calendarService">The calendar service.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        public Event GetEvent(
            CalendarService calendarService,
            string eventId)
        {
            EventsResource.GetRequest request = calendarService.Events.Get(DefaultCalendarId, eventId);

            return request.Execute();
        }
    }
}