 namespace Spectrum.Content.Payments.Models
{
    using System;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    [TableName(Constants.Database.AppointmentTableName)]
    [PrimaryKey("Id", autoIncrement = true)]
    public class PaymentModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1000)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the service provider identifier.
        /// </summary>
        public int ServiceProviderId { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
