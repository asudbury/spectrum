namespace Spectrum.Content.Messages
{
    using Braintree;
    using Umbraco.Web;

    public class PaymentMadeMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMadeMessage" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="autoAllocate">if set to <c>true</c> [automatic allocate].</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        public PaymentMadeMessage(
            UmbracoContext umbracoContext,
            Result<Transaction> transaction,
            string autoAllocate,
            string appointmentId)
        {
            Transaction  = transaction;
            UmbracoContext = umbracoContext;
            AutoAllocate = autoAllocate;
            AppointmentId = appointmentId;
        }

        /// <summary>
        /// Gets the umbraco context.
        /// </summary>
        public UmbracoContext UmbracoContext { get; }

        /// <summary>
        /// The payment identifier.
        /// </summary>
        public Result<Transaction> Transaction { get; }

        /// <summary>
        /// Gets a value indicating whether [automatic allocate].
        /// </summary>
        public string AutoAllocate { get; }

        /// <summary>
        /// Gets the appointment identifier.
        /// </summary>
        public string AppointmentId { get; }
    }
}
