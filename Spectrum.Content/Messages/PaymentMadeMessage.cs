namespace Spectrum.Content.Messages
{
    using Braintree;
    using Umbraco.Web;
    using Payments.ViewModels;

    public class PaymentMadeMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMadeMessage" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="paymentViewModel">The payment view model.</param>
        /// <param name="createdUser">The created user.</param>
        /// <param name="emailTemplateName">Name of the email template.</param>
        public PaymentMadeMessage(
            UmbracoContext umbracoContext,
            Transaction transaction,
            PaymentViewModel paymentViewModel,
            string createdUser,
            string emailTemplateName)
        {
            Transaction  = transaction;
            UmbracoContext = umbracoContext;
            PaymentViewModel = paymentViewModel;
            CreatedUser = createdUser;
            EmailTemplateName = emailTemplateName;
        }

        /// <summary>
        /// Gets the umbraco context.
        /// </summary>
        public UmbracoContext UmbracoContext { get; }

        /// <summary>
        /// The payment identifier.
        /// </summary>
        public Transaction Transaction { get; }

        /// <summary>
        /// Gets the payment view model.
        /// </summary>
        /// <value>
        /// The payment view model.
        /// </value>
        public PaymentViewModel PaymentViewModel { get; }

        /// <summary>
        /// Gets the created user.
        /// </summary>
        public string CreatedUser { get; }

        /// <summary>
        /// Gets the name of the email template.
        /// </summary>
        public string EmailTemplateName { get; }
    }
}
