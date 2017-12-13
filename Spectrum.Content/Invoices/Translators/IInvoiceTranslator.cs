namespace Spectrum.Content.Invoices.Translators
{
    using Models;
    using ViewModels;

    public interface IInvoiceTranslator
    {
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        InvoiceModel Translate(CreateInvoiceViewModel viewModel);

        /// <summary>
        /// Translates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        InvoiceViewModel Translate(InvoiceModel model);
    }
}