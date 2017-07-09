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
        BraintreeModel GetBraintreeModel(UmbracoContext content);

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        string GetAuthToken(BraintreeModel model);

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="braintreeModel">The braintree model.</param>
        /// <param name="model">The model.</param>
        /// <returns>Payment Id</returns>
        string MakePayment(
            BraintreeModel braintreeModel,
            PaymentViewModel model);

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="braintreeModel">The braintree model.</param>
        /// <returns></returns>
        ResourceCollection<Transaction> GetTransactions(BraintreeModel braintreeModel);

    }
}
