namespace Spectrum.Content.Appointments.Services
{
    using Google.Apis.Calendar.v3;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3.Data;

    public interface IGoogleCalendarServices
    {
        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        /// <returns></returns>
        UserCredential GetCredentials(
            string clientId,
            string clientSecret,
            string redirectUrl);

        /// <summary>
        /// Gets the calendar service.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <returns></returns>
        CalendarService GetCalendarService(
            UserCredential userCredential, 
            string applicationName);


        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="calendarService">The calendar service.</param>
        /// <param name="newEvent">The new event.</param>
        /// <returns></returns>
        Event InsertEvent(
            CalendarService calendarService,
            Event newEvent);

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="calendarService">The calendar service.</param>
        /// <returns></returns>
        Events GetEvents(CalendarService calendarService);

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="calendarService">The calendar service.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        Event GetEvent(
            CalendarService calendarService,
            string eventId);
    }
}
