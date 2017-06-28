namespace Spectrum.Content.Appointments.Factories
{
    using Content.Services;
    using Providers;
    using Services;
    using Translators;

    public class CalendarFactory : ICalendarFactory
    {
        /// <summary>
        /// Gets the calendar provider.
        /// </summary>
        /// <param name="calendarType">Type of the calendar.</param>
        /// <returns></returns>
        public ICalendarProvider GetCalendarProvider(string calendarType)
        {
            //// TODO : need to use autofac to resolve.
            return new GoogleCalendarProvider(
                new GoogleCalendarServices(), 
                new GoogleEventTranslator(),
                new AppointmentsProvider(new SettingsService()));
        }
    }
}

