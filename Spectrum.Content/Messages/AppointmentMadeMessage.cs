namespace Spectrum.Content.Messages
{
    public class AppointmentMadeMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentMadeMessage"/> class.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        public AppointmentMadeMessage(string appointmentId)
        {
            AppointmentId = appointmentId;
        }

        /// <summary>
        /// Gets the appointment identifier.
        /// </summary>
        public string AppointmentId { get; }
    }
}
