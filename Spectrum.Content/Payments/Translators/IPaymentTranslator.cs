namespace Spectrum.Content.Payments.Translators
{
    using Braintree;
    using Models;

    public interface IPaymentTranslator
    {
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        PaymentModel Translate(Transaction transaction);
    }
}