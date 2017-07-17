namespace Spectrum.Content.Payments.Providers
{
    using Braintree;
    using ContentModels;
    using Content.Services;
    using Services;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class PaymentProvider : IPaymentProvider
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The braintree service.
        /// </summary>
        private readonly IBraintreeService braintreeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProvider" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="braintreeService">The braintree service.</param>
        public PaymentProvider(
            ISettingsService settingsService,
            IBraintreeService braintreeService)
        {
            this.settingsService = settingsService;
            this.braintreeService = braintreeService;
        }

        /// <summary>
        /// Gets the braintree model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public BraintreeSettingsModel GetBraintreeModel(UmbracoContext umbracoContext)
        {
            IPublishedContent content = settingsService.GetPaymentsNode(umbracoContext);

            return new BraintreeSettingsModel(content);
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string GetAuthToken(BraintreeSettingsModel model)
        {
            return braintreeService.GetAuthToken(model);
        }

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Payment Id</returns>
        public string MakePayment(
            BraintreeSettingsModel model,
            PaymentViewModel viewModel)
        {
            return braintreeService.MakePayment(model, viewModel);
        }

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ResourceCollection<Transaction> GetTransactions(BraintreeSettingsModel model)
        {
            return braintreeService.GetTransactions(model);
        }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        public Transaction GetTransaction(
            BraintreeSettingsModel model,
            string transactionId)
        {
            return braintreeService.GetTransaction(model, transactionId);
        }
    }
}
