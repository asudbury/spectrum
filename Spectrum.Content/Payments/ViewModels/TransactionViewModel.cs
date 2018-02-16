namespace Spectrum.Content.Payments.ViewModels
{
    using System;

    public class TransactionViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the transaction date time.
        /// </summary>
        public DateTime? TransactionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the payment provider.
        /// </summary>
        public string PaymentProvider { get; set; }

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
        /// Gets or sets the amount.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the view transaction URL.
        /// </summary>
        public string ViewTransactionUrl { get; set; }

        /// <summary>
        /// Gets or sets the view invoice URL.
        /// </summary>
        public string ViewInvoiceUrl { get; set; }
    }
}
