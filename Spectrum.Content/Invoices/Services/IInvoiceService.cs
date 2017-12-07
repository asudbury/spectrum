namespace Spectrum.Content.Invoices.Services
{
    using Models;

    public interface IInvoiceService
    {
        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        int CreateInvoice(InvoiceModel model);
    }
}