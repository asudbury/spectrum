namespace Spectrum.Content.Payments.Translators
{
    using Messages;
    using Models;

    public interface IPaymentTranslator
    {
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="paymentMadeMessage">The payment made message.</param>
        /// <returns></returns>
        PaymentModel Translate(PaymentMadeMessage paymentMadeMessage);
    }
}