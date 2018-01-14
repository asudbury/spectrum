namespace Spectrum.Content.Invoices.Managers
{
    using Content.Models;
    using System;
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public interface IInvoiceManager
    {
        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        string CreateInvoice(
            IPublishedContent publishedContent,
            CreateInvoiceViewModel viewModel);

        /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        InvoiceViewModel GetInvoice(
            UmbracoContext umbracoContext,
            string invoiceId);

        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        IEnumerable<InvoiceViewModel> GetInvoices(
            UmbracoContext umbracoContext,
            DateTime dateRangeStart,
            DateTime dateRangeEnd);

        /// <summary>
        /// Gets the boot grid invoices.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        string GetBootGridInvoices(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext,
            DateTime dateRangeStart,
            DateTime dateRangeEnd);
    }
}