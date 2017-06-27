namespace Spectrum.Content.Appointments.Factories
{
    using Providers;

    public class CalendarFactory : ICalendarFactory
    {
        /// <summary>
        /// Gets the calendar provider.
        /// </summary>
        /// <param name="calendarType">Type of the calendar.</param>
        /// <returns></returns>
        public ICalendarProvider GetCalendarProvider(string calendarType)
        {
            return new GoogleCalendarProvider();
        }
    }
}
