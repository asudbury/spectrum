namespace Spectrum.Content.Payments.Translators.Interfaces
{
    using Invoices.Models;
    using Models;
    using ViewModels;

    public interface ITransactionTranslator
    {
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="invoiceModel">The invoice model.</param>
        /// <returns></returns>
        TransactionViewModel Translate(
            TransactionModel model,
            InvoiceModel invoiceModel);
    }
}