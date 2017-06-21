namespace Spectrum.Content.Payments.Translators
{
    using Braintree;
    using ViewModels;

    public interface ITransactionsTranslator
    {
        /// <summary>
        /// Translates the specified transactions.
        /// </summary>
        /// <param name="transactions">The transactions.</param>
        /// <returns></returns>
        TransactionsViewModel Translate(ResourceCollection<Transaction> transactions);
    }
}
