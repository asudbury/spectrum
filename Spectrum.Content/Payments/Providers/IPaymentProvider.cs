namespace Spectrum.Content.Payments.Providers
{
    using Braintree;
    using ContentModels;
    using Umbraco.Web;
    using ViewModels;

    public interface IPaymentProvider
    {
        /// <summary>
        /// Gets the payment settings model.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        PaymentSettingsModel GetPaymentSettingsModel(
            UmbracoContext content,
            int? customerId = null);

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
        Result<Transaction> MakePayment(
            PaymentSettingsModel paymentModel,
            MakePaymentViewModel model);

        /// <summary>
        /// Gets the braintree transactions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ResourceCollection<Transaction> GetBraintreeTransactions(PaymentSettingsModel model);

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
