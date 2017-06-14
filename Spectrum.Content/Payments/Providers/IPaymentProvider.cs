namespace Spectrum.Content.Payments.Providers
{
    using Braintree;
    using ContentModels;
    using ViewModels;

    public interface IPaymentProvider
    {
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
        /// <returns></returns>
        bool MakePayment(
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
