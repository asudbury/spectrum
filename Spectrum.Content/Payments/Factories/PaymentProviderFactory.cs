namespace Spectrum.Content.Payments.Factories
{
    using ContentModels;
    using Umbraco.Core.Models;
    using Content.Services;
    using Umbraco.Web;
    using System;

    public class PaymentProviderFactory : IPaymentProviderFactory
    {
        /// <summary>
        /// The base directory.
        /// </summary>
        private const string BaseDirectory = "~/Views/Partials/Spectrum/Payments/";

        /// <summary>
        /// The braintree directory.
        /// </summary>
        private const string BraintreeDirectory = BaseDirectory + "Braintree/";

        /// <summary>
        /// The pay pal directory.
        /// </summary>
        private const string PayPalDirectory = BaseDirectory + "PayPal/";

        /// <summary>
        /// The payment page.
        /// </summary>
        private const string PaymentPage = "Payment.cshtml";

        /// <summary>
        /// The transactions page.
        /// </summary>
        private const string TransactionsPage = "Transactions.cshtml";

        /// <summary>
        /// The transaction page.
        /// </summary>
        private const string TransactionPage = "Transaction.cshtml";

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProviderFactory"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public PaymentProviderFactory(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the transactions partial view.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="useSpectrumPage">if set to <c>true</c> [use spectrum page].</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Payment Provider not setup.</exception>
        /// <inheritdoc />
        public string GetTransactionsPartialView(
            UmbracoContext umbracoContext,
            bool useSpectrumPage)
        {
            if (useSpectrumPage)
            {
                return BaseDirectory + TransactionsPage;
            }

            PaymentSettingsModel model = GetPaymentSettingsModel(umbracoContext);

            switch (model.Provider)
            {
                case Constants.PaymentProviders.Braintree:
                    return BraintreeDirectory + TransactionsPage;

                case Constants.PaymentProviders.PayPal:
                    return PayPalDirectory + TransactionsPage;

                default:
                    throw new ApplicationException("Payment Provider not setup.");
            }
        }

        /// <summary>
        /// Gets the transaction partial view.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="useSpectrumPage">if set to <c>true</c> [use spectrum page].</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Payment Provider not setup.</exception>
        /// <inheritdoc />
        public string GetTransactionPartialView(
            UmbracoContext umbracoContext,
            bool useSpectrumPage)
        {
            if (useSpectrumPage)
            {
                return BaseDirectory + TransactionPage;
            }

            PaymentSettingsModel model = GetPaymentSettingsModel(umbracoContext);

            switch (model.Provider)
            {
                case Constants.PaymentProviders.Braintree:
                    return BraintreeDirectory + TransactionPage;

                case Constants.PaymentProviders.PayPal:
                    return PayPalDirectory + TransactionPage;

                default:
                    throw new ApplicationException("Payment Provider not setup.");
            }
        }

        /// <summary>
        /// Gets the payment partial view.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Payment Provider not setup.</exception>
        /// <inheritdoc />
        public string GetPaymentPartialView(
            UmbracoContext umbracoContext)
        {
            PaymentSettingsModel model = GetPaymentSettingsModel(umbracoContext);

            switch (model.Provider)
            {
                case Constants.PaymentProviders.Braintree:
                    return BraintreeDirectory + PaymentPage;

                case Constants.PaymentProviders.PayPal:
                    return PayPalDirectory + PaymentPage;

                default:
                    throw new ApplicationException("Payment Provider not setup.");
            }
        }

        /// <summary>
        /// Gets the payment settings model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        internal PaymentSettingsModel GetPaymentSettingsModel(UmbracoContext umbracoContext)
        {
            IPublishedContent  paymentsNode = settingsService.GetPaymentsNode(umbracoContext);

            return new PaymentSettingsModel(paymentsNode);
        }
    }
}
