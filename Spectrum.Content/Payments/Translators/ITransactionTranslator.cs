namespace Spectrum.Content.Payments.Translators
{
    using Braintree;
    using ViewModels;

    public interface ITransactionTranslator
    {
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        TransactionViewModel Translate(Transaction transaction);
    }
}

