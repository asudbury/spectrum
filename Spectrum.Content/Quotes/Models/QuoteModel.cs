namespace Spectrum.Content.Quotes.Models
{
    using Content.Models;
    using System;
    using Umbraco.Core.Persistence.DatabaseAnnotations;
    using Umbraco.Core.Persistence;

    [TableName(Constants.Database.QuoteTableName)]
    [PrimaryKey("QuoteId", autoIncrement = true)]
    public class QuoteModel : BaseClientModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column("QuoteId")]
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1000)]
        public int QuoteId { get; set; }

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
