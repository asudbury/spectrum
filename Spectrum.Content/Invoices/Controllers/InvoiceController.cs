namespace Spectrum.Content.Invoices.Controllers
{
    using Content.Models;
    using Content.Services;
    using Customer.Managers;
    using Managers;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using ViewModels;

    public class InvoiceController : BaseController
    {
        /// <summary>
        /// The rules engine service.
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// The invoice manager.
        /// </summary>
        private readonly IInvoiceManager invoiceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <param name="invoiceManager">The invoice manager.</param>
        /// <inheritdoc />
        public InvoiceController(
            ILoggingService loggingService,
            IRulesEngineService rulesEngineService,
            IInvoiceManager invoiceManager)
            : base(loggingService)
        {
            this.rulesEngineService = rulesEngineService;
            this.invoiceManager = invoiceManager;
        }

        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetInvoices()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerInvoicesEnabled())
            {
                return PartialView("Partials/Spectrum/Invoices/Invoices");
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Views the invoice.
        /// </summary>
        /// <param name="fkdssre">The fkdssre.</param>
        /// <param name="fdwpoe">The fdwpoe.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult ViewInvoice(
            string fkdssre,
            string fdwpoe)
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerInvoicesEnabled())
            {
                InvoiceViewModel viewModel = invoiceManager.GetInvoice(UmbracoContext, fdwpoe);

                return PartialView("Partials/Spectrum/Invoices/Invoice", viewModel);
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Gets the Create an Invoice.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetCreateInvoice(string fkdssre)
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerInvoicesEnabled())
            {
                CreateInvoiceViewModel viewModel = new CreateInvoiceViewModel { Code = fkdssre };

                return PartialView("Partials/Spectrum/Invoices/CreateInvoice", viewModel);
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Gets the boot grid invoices.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBootGridInvoices(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems)
        {
            LoggingService.Info(GetType());

            DateTime dateRangeStart = DateTime.Now.AddDays(-10000);
            DateTime dateRangeEnd = DateTime.Now.AddDays(10000);

            string jsonString = invoiceManager.GetBootGridInvoices(
                current,
                rowCount,
                searchPhrase,
                sortItems,
                UmbracoContext,
                dateRangeStart,
                dateRangeEnd);

            return Content(jsonString, "application/json");
        }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInvoice(CreateInvoiceViewModel viewModel)
        {
            if (rulesEngineService.IsCustomerInvoicesEnabled() == false)
            {
                ThrowAccessDeniedException("No Access to create invoice");
            }

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            IPublishedContent publishedContent = GetContentById(CurrentPage.Id.ToString());

            string nextUrl = invoiceManager.CreateInvoice(
                UmbracoContext,
                publishedContent,
                viewModel);

            return Redirect(nextUrl);
        }
    }
}
