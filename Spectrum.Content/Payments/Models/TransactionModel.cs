 namespace Spectrum.Content.Payments.Models
{
    using System;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    [TableName(Constants.Database.TransactionsTableName)]
    [PrimaryKey("Id", autoIncrement = true)]
    public class TransactionModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1000)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction.
        /// (S)ale, (C)redit
        /// </summary>
        [Length(1)]
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the paymemt provider.
        /// </summary>
        [Length(1)]
        public string PaymemtProvider { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        [Length(1)]
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the service provider identifier.
        /// </summary>
        [NullSetting(NullSetting = NullSettings.Null)]
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
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the type of the card.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Gets or sets the masked card number.
        /// </summary>
        public string MaskedCardNumber { get; set; }
    }
}
