namespace Spectrum.Content.Payments.Providers
{
    using Braintree;
    using ContentModels;
    using Services;
    using ViewModels;
    using Umbraco.Core.Models;

    public class PaymentProvider : IPaymentProvider
    {
        /// <summary>
        /// The braintree service.
        /// </summary>
        private readonly IBraintreeService braintreeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProvider"/> class.
        /// </summary>
        /// <param name="braintreeService">The braintree service.</param>
        public PaymentProvider(IBraintreeService braintreeService)
        {
            this.braintreeService = braintreeService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProvider"/> class.
        /// </summary>
        public PaymentProvider()
            : this(new Services.BraintreeService())
        {
        }

        /// <summary>
        /// Gets the braintree model.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public BraintreeModel GetBraintreeModel(IPublishedContent content)
        {
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
