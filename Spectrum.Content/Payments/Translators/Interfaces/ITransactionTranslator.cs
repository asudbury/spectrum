namespace Spectrum.Content.Payments.Translators.Interfaces
{
    using Models;
    using ViewModels;

    public interface ITransactionTranslator
    {
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionViewModel Translate(TransactionModel model);
    }
}