namespace Spectrum.Content.Messages
{
    using Braintree;
    using Customer.ViewModels;
    using Payments.ViewModels;
    using Umbraco.Web;

    public class TransactionMadeMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionMadeMessage" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="paymentViewModel">The payment view model.</param>
        /// <param name="clientViewModel">The client view model.</param>
        /// <param name="createdUser">The created user.</param>
        /// <param name="emailTemplateName">Name of the email template.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="environment">The environment.</param>
        public TransactionMadeMessage(
            UmbracoContext umbracoContext,
            Transaction transaction,
            MakePaymentViewModel paymentViewModel,
            ClientViewModel clientViewModel,
            string createdUser,
            string emailTemplateName,
            string paymentProvider,
            string environment)
        {
            Transaction  = transaction;
            UmbracoContext = umbracoContext;
            PaymentViewModel = paymentViewModel;
            ClientViewModel = clientViewModel;
            CreatedUser = createdUser;
            EmailTemplateName = emailTemplateName;
            PaymentProvider = paymentProvider;
            Environment = environment;
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
        public MakePaymentViewModel PaymentViewModel { get; }

        /// <summary>
        /// Gets the client view model.
        /// </summary>
        public ClientViewModel ClientViewModel { get; }

        /// <summary>
        /// Gets the created user.
        /// </summary>
        public string CreatedUser { get; }

        /// <summary>
        /// Gets the name of the email template.
        /// </summary>
        public string EmailTemplateName { get; }

        /// <summary>
        /// Gets the payment provider.
        /// </summary>
        public string PaymentProvider { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        public string Environment { get; }
    }
}
