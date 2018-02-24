namespace Spectrum.Content.Payments.Providers
{
    using Braintree;
    using Content.Services;
    using ContentModels;
    using Services;
    using System;
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
        /// Gets the payment settings model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public PaymentSettingsModel GetPaymentSettingsModel(
            UmbracoContext umbracoContext,
            int? customerId = null)
        {
            IPublishedContent content = settingsService.GetPaymentsNode(customerId);

            return content != null ? new PaymentSettingsModel(content) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string GetAuthToken(PaymentSettingsModel model)
        {
            string authToken = braintreeService.GetAuthToken(model);

            if (string.IsNullOrEmpty(authToken))
            {
                throw new ApplicationException("Invalid AuthToken");
            }

            return authToken;

        }

        /// <inheritdoc />
        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Payment Id</returns>
        public Result<Transaction> MakePayment(
            PaymentSettingsModel model,
            MakePaymentViewModel viewModel)
        {
            return braintreeService.MakePayment(model, viewModel);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ResourceCollection<Transaction> GetBraintreeTransactions(PaymentSettingsModel model)
        {
            return braintreeService.GetTransactions(model);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        public Transaction GetTransaction(
            PaymentSettingsModel model,
            string transactionId)
        {
            return braintreeService.GetTransaction(model, transactionId);
        }
    }
}
