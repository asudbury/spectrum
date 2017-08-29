namespace Spectrum.Content.Appointments.Services
{
    using Models;

    public interface IICalendarService
    {
        /// <summary>
        /// Gets the ical appoinment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        ICalAppointmentModel GetICalAppoinment(
            AppointmentModel model,
            string guid = null,
            int sequence = 0);
    }
}
