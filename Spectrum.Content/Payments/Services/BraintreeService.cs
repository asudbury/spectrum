namespace Spectrum.Content.Payments.Services
{
    using Braintree;
    using ContentModels;
    using ViewModels;

    public class BraintreeService : IBraintreeService
    {
        /// <summary>
        /// Gets the gateway.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public IBraintreeGateway GetGateway(BraintreeModel model)
        {
            return new BraintreeGateway(
                        model.Environment, 
                        model.MerchantId, 
                        model.PublicKey, 
                        model.PrivateKey);
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// The token.
        /// </returns>
        public string GetAuthToken(BraintreeModel model)
        {
            return GetGateway(model).ClientToken.generate();
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

            if (result.IsSuccess())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ResourceCollection<Transaction> GetTransactions(BraintreeModel model)
        {
            TransactionSearchRequest request = new TransactionSearchRequest();

            return GetGateway(model).Transaction.Search(request);
        }
    }
}
