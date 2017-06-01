namespace Spectrum.Content.Payments.Services
{
    using Braintree;

    public interface IBraintreeService
    {
        /// <summary>
        /// Gets the gateway.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key.</param>
        /// <returns></returns>
        IBraintreeGateway GetGateway(
            string environment,
            string merchantId,
            string publicKey,
            string privateKey);

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>The token.</returns>
        string GetToken();

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="nonce">The nonce.</param>
        /// <returns></returns>
        bool MakePayment(
            decimal amount,
            string nonce);
    }
}
