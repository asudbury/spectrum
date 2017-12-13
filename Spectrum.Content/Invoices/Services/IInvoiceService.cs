namespace Spectrum.Content.Invoices.Services
{
    using Models;
    using System;

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
        /// Gets the invoices.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        IEnumerable<InvoiceModel> GetInvoices(
            DateTime dateRangeStart,
            DateTime dateRangeEnd,
            int customerId);
    }
}