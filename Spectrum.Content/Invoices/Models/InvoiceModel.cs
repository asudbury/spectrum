namespace Spectrum.Content.Invoices.Models
{
    using System;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    [TableName(Constants.Database.InvoiceTableName)]
    [PrimaryKey("Id", autoIncrement = true)]
    public class InvoiceModel
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
        /// Gets or sets the client identifier.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the address identifier.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime CreatedTime { get; set; }
        
        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime LasteUpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string LastedUpdatedUser { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the invoice amount.
        /// </summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// Gets or sets the invoice details.
        /// </summary>
        public string InvoiceDetails { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        [NullSetting(NullSetting = NullSettings.Null)]
        public string PaymentId { get; set; }
    }
}
