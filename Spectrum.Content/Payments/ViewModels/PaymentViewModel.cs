namespace Spectrum.Content.Payments.ViewModels
{
    /// <summary>
    /// The PaymentViewModel class.
    /// </summary>
    public class PaymentViewModel
    {
        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        public string NodeId { get; set; }

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
