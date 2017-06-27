namespace Spectrum.Content.Appointments.Services
{
    using Models;
    using ViewModels;

    public interface IICalendarService
    {
        /// <summary>
        /// Gets the i calendar string.
        /// </summary>
        /// <param name="eventViewModel">The event view model.</param>
        /// <returns></returns>
        ICalEventModel GetICalendarString(EventViewModel eventViewModel);
    }
}
