namespace Spectrum.Content.Appointments.Services
{
    using Models;
    using ViewModels;

    public interface IICalendarService
    {
        /// <summary>
        /// Gets the i calendar string.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        ICalEventModel GetICalendarString(InsertAppointmentViewModel viewModel);
    }
}
