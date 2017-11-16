namespace Spectrum.Content.Payments.Translators.Interfaces
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
        TransactionModel Translate(TransactionMadeMessage paymentMadeMessage);
    }
}