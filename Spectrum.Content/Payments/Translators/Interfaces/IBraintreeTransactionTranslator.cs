namespace Spectrum.Content.Payments.Translators.Interfaces
{
    using Braintree;
    using ViewModels;

    public interface IBraintreeTransactionTranslator
    {
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        BraintreeTransactionViewModel Translate(Transaction transaction);
    }
}

