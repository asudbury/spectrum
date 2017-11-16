namespace Spectrum.Content.Payments.ViewModels
{
    using System;

    public class BraintreeTransactionViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction date time.
        /// </summary>
        public DateTime? TransactionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the type of the card.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Gets or sets the masked number.
        /// </summary>
        public string MaskedNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the currency.
        /// </summary>

        public string CurrencyType { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public Decimal? Amount { get; set; }
    }
}
