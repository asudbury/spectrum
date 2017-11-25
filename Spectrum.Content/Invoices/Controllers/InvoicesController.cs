using Spectrum.Content.Invoices.ViewModels;

namespace Spectrum.Content.Invoices.Controllers
{
    using Services;
    using System.Web.Mvc;
    using Umbraco.Web;

    public class InvoicesController : BaseController
    {
        /// <summary>
        /// The rules engine service.
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public InvoicesController(
            ILoggingService loggingService,
            IRulesEngineService rulesEngineService) 
            : base(loggingService)
        {
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public InvoicesController(
            ILoggingService loggingService, 
            UmbracoContext umbracoContext,
            IRulesEngineService rulesEngineService) 
            : base(loggingService, umbracoContext)
        {
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public InvoicesController(
            ILoggingService loggingService, 
            UmbracoContext umbracoContext, 
            UmbracoHelper umbracoHelper,
            IRulesEngineService rulesEngineService) 
            : base(loggingService, umbracoContext, umbracoHelper)
        {
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
                return PartialView("Partials/Spectrum/Invoices/CreateInvoice", new CreateInvoiceViewModel());
            }

            return default(PartialViewResult);
        }

        public void CreateInvoice(CreateInvoiceViewModel viewModel)
        {
            
        }
    }
}
