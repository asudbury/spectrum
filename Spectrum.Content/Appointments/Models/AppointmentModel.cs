namespace Spectrum.Content.Appointments.Models
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    [TableName(Constants.Database.AppointmentTableName)]
    [PrimaryKey("Id", autoIncrement = true)]
    public class AppointmentModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1000)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string LastedUpdatedUser { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime LasteUpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        [NullSetting(NullSetting = NullSettings.Null)]
        public int InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        [NullSetting(NullSetting = NullSettings.Null)]
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the service provider identifier.
        /// </summary>
        [NullSetting(NullSetting = NullSettings.Null)]
        public int ServiceProviderId { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        [Ignore]
        public List<AppointmentAttendeeModel> Attendees { get; set; }
    }
}
