namespace Spectrum.Content.Payments.Services
{
    using Braintree;

    public class BraintreeService : IBraintreeService
    {
        /// <summary>
        /// The braintree gateway.
        /// </summary>
        private IBraintreeGateway braintreeGateway;

        /// <summary>
        /// Gets the gateway.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key.</param>
        /// <returns></returns>
        public IBraintreeGateway GetGateway(
            string environment, 
            string merchantId, 
            string publicKey, 
            string privateKey)
        {
            return braintreeGateway ?? (braintreeGateway = new BraintreeGateway(
                                                                environment, 
                                                                merchantId, 
                                                                publicKey, 
                                                                privateKey));
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>The token.</returns>
        public string GetToken()
        {
            return braintreeGateway.ClientToken.generate();
        }

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="nonce">The nonce.</param>
        /// <returns></returns>
        public bool MakePayment(
            decimal amount,
            string nonce)
        {
            TransactionRequest request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = braintreeGateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                return true;
            }

            return false;
        }
    }
}
