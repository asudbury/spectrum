namespace Spectrum.Content.Payments.ViewModels
{
    /// <summary>
    /// The PaymentViewModel class.
    /// </summary>
    public class PaymentViewModel
    {
        /// <summary>
        /// Gets or sets the current page node identifier.
        /// </summary>
        public string CurrentPageNodeId { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the nonce.
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
