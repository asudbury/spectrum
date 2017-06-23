namespace Spectrum.Content.Appointments.Models
{
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    [TableName("SpectrumAppointment")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class AppointmentModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        [Column("PaymentId")]
        public string PaymentId { get; set; }
        
        /// <summary>
        /// Gets or sets the appointment identifier.
        /// </summary>

        [Column("AppointmentId")]
        public string AppointmentId { get; set; }
    }
}
