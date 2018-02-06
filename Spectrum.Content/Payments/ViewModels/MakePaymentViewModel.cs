namespace Spectrum.Content.Payments.ViewModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// The MakePaymentViewModel class.
    /// </summary>
    public class MakePaymentViewModel
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        [JsonProperty(PropertyName = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        [JsonProperty(PropertyName = "invoiceId")]
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the current page node identifier.
        /// </summary>
        [JsonProperty(PropertyName = "currentPageNodeId")]
        public string CurrentPageNodeId { get; set; }

        /// <summary>
        /// Gets or sets the nonce.
        /// </summary>
        [JsonProperty(PropertyName = "nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }
    }
}
