namespace Spectrum.Content.Invoices.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using Umbraco.Core;
    using Umbraco.Core.Persistence;

    public class InvoiceService : IInvoiceService
    {
        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int CreateInvoice(InvoiceModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            object invoiceId = context.Database.Insert(model);

            return Convert.ToInt32(invoiceId);
        }

        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public IEnumerable<InvoiceModel> GetInvoices(
            DateTime dateRangeStart, 
            DateTime dateRangeEnd, 
            int customerId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(Content.Constants.Database.InvoiceTableName)
                .Where("CustomerId=" + customerId)
                .OrderByDescending("InvoiceDate");

            return context.Database.Fetch<InvoiceModel>(sql);
        }
    }
}
