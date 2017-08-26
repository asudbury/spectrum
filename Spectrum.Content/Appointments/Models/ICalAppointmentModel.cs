namespace Spectrum.Content.Appointments.Models
{
    using System.Net.Mime;

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

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        public ContentType ContentType { get; set; }
    }
}
