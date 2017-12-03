namespace Spectrum.Content.Invoices.Controllers
{
    using ContentModels;
    using Services;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web;
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
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public InvoicesController(
            ILoggingService loggingService,
            ISettingsService settingsService,
            IRulesEngineService rulesEngineService) 
            : base(loggingService)
        {
            this.settingsService = settingsService;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public InvoicesController(
            ILoggingService loggingService,
            ISettingsService settingsService,
            UmbracoContext umbracoContext,
            IRulesEngineService rulesEngineService) 
            : base(loggingService, umbracoContext)
        {
            this.settingsService = settingsService;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public InvoicesController(
            ILoggingService loggingService,
            ISettingsService settingsService,
            UmbracoContext umbracoContext, 
            UmbracoHelper umbracoHelper,
            IRulesEngineService rulesEngineService) 
            : base(loggingService, umbracoContext, umbracoHelper)
        {
            this.settingsService = settingsService;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetInvoices()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerInvoicesEnabled(UmbracoContext))
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

            if (rulesEngineService.IsCustomerInvoicesEnabled(UmbracoContext))
            {
                CreateInvoiceViewModel viewModel = new CreateInvoiceViewModel();
                
                IPublishedContent paymentsNode = settingsService.GetPaymentsNode(UmbracoContext);

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
        public void CreateInvoice(CreateInvoiceViewModel viewModel)
        {
            if (rulesEngineService.IsCustomerInvoicesEnabled(UmbracoContext) == false)
            {
                ThrowAccessDeniedException("No Access to create invoice");
            }
        }
    }
}
