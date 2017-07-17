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
        BraintreeSettingsModel GetBraintreeModel(UmbracoContext content);

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        string GetAuthToken(BraintreeSettingsModel model);

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="braintreeModel">The braintree model.</param>
        /// <param name="model">The model.</param>
        /// <returns>Payment Id</returns>
        string MakePayment(
            BraintreeSettingsModel braintreeModel,
            PaymentViewModel model);

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ResourceCollection<Transaction> GetTransactions(BraintreeSettingsModel model);

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        Transaction GetTransaction(
            BraintreeSettingsModel model,
            string transactionId);

    }
}
