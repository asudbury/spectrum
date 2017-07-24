namespace Spectrum.Content.Appointments.Models
{
    // ReSharper disable once InconsistentNaming
    public class ICalAppointmentModel
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the serialized string.
        /// </summary>
        public string SerializedString { get; set; }
    }
}
