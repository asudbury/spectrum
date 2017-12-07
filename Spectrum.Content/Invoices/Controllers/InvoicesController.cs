namespace Spectrum.Content.Invoices.Controllers
{
    using Content.Services;
    using ContentModels;
    using Managers;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using ViewModels;

    public class InvoicesController : BaseController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

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
        /// <param name="settingsService">The settings service.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <param name="invoiceManager">The invoice manager.</param>
        /// <inheritdoc />
        public InvoicesController(
            ILoggingService loggingService,
            ISettingsService settingsService,
            IRulesEngineService rulesEngineService,
            IInvoiceManager invoiceManager)
            : base(loggingService)
        {
            this.settingsService = settingsService;
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
        /// Gets the Create an Invoice.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetCreateInvoice()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerInvoicesEnabled())
            {
                CreateInvoiceViewModel viewModel = new CreateInvoiceViewModel();
                
                IPublishedContent paymentsNode = settingsService.GetPaymentsNode();

                PaymentSettingsModel settingsModel = new PaymentSettingsModel(paymentsNode);

                viewModel.ShowIncludePaymentLink = settingsModel.CustomerPaymentsEnabled;

                return PartialView("Partials/Spectrum/Invoices/CreateInvoice", viewModel);
            }

            return default(PartialViewResult);
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

            invoiceManager.CreateInvoice(viewModel);

            return default(PartialViewResult);
        }
    }
}
