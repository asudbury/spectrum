namespace Spectrum.Content.Payments.Services
{
    using Braintree;
    using ContentModels;
    using ViewModels;

    public interface IBraintreeService
    {
        /// <summary>
        /// Gets the gateway.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        IBraintreeGateway GetGateway(PaymentSettingsModel model);

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The token.</returns>
        string GetAuthToken(PaymentSettingsModel model);

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Payment Id</returns>
        Result<Transaction> MakePayment(
            PaymentSettingsModel model,
            PaymentViewModel viewModel);

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
