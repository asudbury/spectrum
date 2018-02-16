namespace Spectrum.Content.Invoices.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IInvoiceService
    {
        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        int CreateInvoice(InvoiceModel model);

        /// <summary>
        /// Gets the client invoice.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        ClientInvoiceModel GetClientInvoice(
            int customerId,
            string invoiceId);

        /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        InvoiceModel GetInvoice(
            int customerId,
            string invoiceId);

        /// <summary>
        /// Gets the invoice by payment identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="paymentId">The payment identifier.</param>
        /// <returns></returns>
        InvoiceModel GetInvoiceByPaymentId(
            int customerId,
            string paymentId);

        /// <summary>
        /// Gets the client invoices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        IEnumerable<ClientInvoiceModel> GetClientInvoices(int customerId);

        /// <summary>
        /// Updates the invoice.
        /// </summary>
        /// <param name="model">The model.</param>
        void UpdateInvoice(InvoiceModel model);
    }
}