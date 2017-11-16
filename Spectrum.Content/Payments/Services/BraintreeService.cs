namespace Spectrum.Content.Payments.Services
{
    using Braintree;
    using ContentModels;
    using ViewModels;

    public class BraintreeService : IBraintreeService
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets the gateway.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public IBraintreeGateway GetGateway(PaymentSettingsModel model)
        {
            return new BraintreeGateway(
                model.Environment,
                model.MerchantId,
                model.PublicKey,
                model.PrivateKey);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// The token.
        /// </returns>
        public string GetAuthToken(PaymentSettingsModel model)
        {
            return GetGateway(model).ClientToken.generate();
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
            TransactionRequest request = new TransactionRequest
            {
                Amount = viewModel.Amount,
                PaymentMethodNonce = viewModel.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = GetGateway(model).Transaction.Sale(request);

            if (result.IsSuccess() &&
                result.Target != null)
            {
                return result;
            }

            return null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ResourceCollection<Transaction> GetTransactions(PaymentSettingsModel model)
        {
            TransactionSearchRequest request = new TransactionSearchRequest();

            return GetGateway(model).Transaction.Search(request);
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
            return GetGateway(model).Transaction.Find(transactionId);
        }
    }
}
