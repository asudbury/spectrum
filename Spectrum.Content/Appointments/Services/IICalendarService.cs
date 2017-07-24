namespace Spectrum.Content.Appointments.Services
{
    using Models;

    public interface IICalendarService
    {
        /// <summary>
        /// Gets the ical appoinment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ICalAppointmentModel GetICalAppoinment(AppointmentModel model);
    }
}
