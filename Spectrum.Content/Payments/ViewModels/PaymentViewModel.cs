namespace Spectrum.Content.Payments.ViewModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// The PaymentViewModel class.
    /// </summary>
    public class PaymentViewModel
    {
        /// <summary>
        /// Gets or sets the current page node identifier.
        /// </summary>
        [JsonProperty(PropertyName = "currentPageNodeId")]
        public string CurrentPageNodeId { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [JsonProperty(PropertyName = "emailAddress")]
        public string EmailAddress { get; set; }

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

        /// <summary>
        /// Gets or sets a value indicating whether [automatic allocate].
        /// </summary>
        [JsonProperty(PropertyName = "autoAllocate")]
        public string AutoAllocate { get; set; }
        
        /// <summary>
        /// Gets or sets the appointment identifier.
        /// </summary>
        [JsonProperty(PropertyName = "appointmentId")]
        public string AppointmentId { get; set; }
    }
}
