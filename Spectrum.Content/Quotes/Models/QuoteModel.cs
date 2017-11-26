namespace Spectrum.Content.Quotes.Models
{
    using System;
    using Umbraco.Core.Persistence.DatabaseAnnotations;
    using Umbraco.Core.Persistence;

    [TableName(Constants.Database.QuoteTableName)]
    [PrimaryKey("Id", autoIncrement = true)]
    public class QuoteModel
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
        /// Gets or sets the quote date.
        /// </summary>
        public DateTime QuoteDate { get; set; }

        /// <summary>
        /// Gets or sets the quote amount.
        /// </summary>
        public decimal QuoteAmount { get; set; }

        /// <summary>
        /// Gets or sets the quote details.
        /// </summary>
        public string QuoteDetails { get; set; }
    }
}
