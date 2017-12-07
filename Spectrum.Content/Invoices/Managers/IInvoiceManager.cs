namespace Spectrum.Content.Invoices.Managers
{
    using ViewModels;

    public interface IInvoiceManager
    {
        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        void CreateInvoice(CreateInvoiceViewModel viewModel);
    }
}