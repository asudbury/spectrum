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
        public BraintreeModel GetBraintreeModel(UmbracoContext umbracoContext)
        {
            IPublishedContent content = settingsService.GetPaymentsNode(umbracoContext);

            return new BraintreeModel(content);
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string GetAuthToken(BraintreeModel model)
        {
            return braintreeService.GetAuthToken(model);
        }

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public bool MakePayment(
            BraintreeModel model,
            PaymentViewModel viewModel)
        {
            return braintreeService.MakePayment(model, viewModel);
        }

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ResourceCollection<Transaction> GetTransactions(BraintreeModel model)
        {
            return braintreeService.GetTransactions(model);
        }
    }
}
