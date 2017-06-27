namespace Spectrum.Content.Appointments.Factories
{
    using Providers;

    public interface ICalendarFactory
    {
        /// <summary>
        /// Gets the calendar provider.
        /// </summary>
        /// <param name="calendarType">Type of the calendar.</param>
        /// <returns></returns>
        ICalendarProvider GetCalendarProvider(string calendarType);
    }
}
