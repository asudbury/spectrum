namespace Spectrum.Content.Appointments.Models
{
    using System;
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
        public string PaymentId { get; set; }
        
        /// <summary>
        /// Gets or sets the appointment identifier.
        /// </summary>
        
        public string AppointmentId { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
