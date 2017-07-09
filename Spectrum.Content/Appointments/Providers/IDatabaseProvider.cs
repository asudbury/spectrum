namespace Spectrum.Content.Appointments.Providers
{
    using Models;

    public interface IDatabaseProvider
    {
        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The appointmentId</returns>
        string InsertAppointment(AppointmentModel model);
    }
}
