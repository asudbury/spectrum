namespace Spectrum.Content.Messages
{
    using Umbraco.Web;

    public class PaymentMadeMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMadeMessage" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="paymentId">The payment identifier.</param>
        public PaymentMadeMessage(
            UmbracoContext umbracoContext,
            string paymentId)
        {
            PaymentId = paymentId;
            UmbracoContext = umbracoContext;
        }

        /// <summary>
        /// Gets the umbraco context.
        /// </summary>
        public UmbracoContext UmbracoContext { get; }

        /// <summary>
        /// The payment identifier.
        /// </summary>
        public string PaymentId { get; }
    }
}
