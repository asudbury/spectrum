namespace Spectrum.Content.Payments.Providers
{
    using Braintree;
    using ContentModels;
    using Umbraco.Web;
    using ViewModels;

    public interface IPaymentProvider
    {
        /// <summary>
        /// Gets the braintree model.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        PaymentSettingsModel GetBraintreeModel(UmbracoContext content);

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        string GetAuthToken(PaymentSettingsModel model);

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="paymentModel">The braintree model.</param>
        /// <param name="model">The model.</param>
        /// <returns>Payment Id</returns>
        string MakePayment(
            PaymentSettingsModel paymentModel,
            PaymentViewModel model);

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ResourceCollection<Transaction> GetTransactions(PaymentSettingsModel model);

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        Transaction GetTransaction(
            PaymentSettingsModel model,
            string transactionId);

    }
}
