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
            //// catch client id not set - might occur if code is regressed!
            if (model.ClientId == 0)
            {
                throw new ApplicationException("Create Invoice - Client Id not set");
            }

            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            object invoiceId = context.Database.Insert(model);

            return Convert.ToInt32(invoiceId);
        }

        /// <summary>
        /// Gets the client invoice.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        public ClientInvoiceModel GetClientInvoice(
            int customerId, 
            string invoiceId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("sc.Name as ClientName, si.*")
                .From(Content.Constants.Database.InvoiceTableName + " si")
                .InnerJoin(Content.Constants.Database.ClientTableName + " sc")
                .On("si.ClientId = sc.Id")
                .Where("si.CustomerId = @0", customerId)
                .Where("si.Id = @0", invoiceId);

            return context.Database.FirstOrDefault<ClientInvoiceModel>(sql);
        }

        /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        public InvoiceModel GetInvoice(
            int customerId, 
            string invoiceId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(Content.Constants.Database.InvoiceTableName + " si")
                .Where("si.CustomerId = @0", customerId)
                .Where("si.Id = @0", invoiceId);

            return context.Database.FirstOrDefault<InvoiceModel>(sql);
        }

        /// <summary>
        /// Gets the client invoices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public IEnumerable<ClientInvoiceModel> GetClientInvoices(int customerId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("sc.Name as ClientName, si.*")
                .From(Content.Constants.Database.InvoiceTableName + " si")
                .InnerJoin(Content.Constants.Database.ClientTableName + " sc")
                .On("si.ClientId = sc.Id")
                .Where("si.CustomerId = @0", customerId)
                .OrderByDescending("InvoiceDate", "Id");

            return context.Database.Fetch<ClientInvoiceModel>(sql);
        }

        /// <summary>
        /// Updates the invoice.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UpdateInvoice(InvoiceModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            context.Database.Update(model);
        }
    }
}
