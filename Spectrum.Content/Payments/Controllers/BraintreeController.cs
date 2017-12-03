namespace Spectrum.Content.Payments.Controllers
{
    using Content.Models;
    using Content.Services;
    using Factories;
    using Managers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Umbraco.Web;
    using ViewModels;

    public class BraintreeController : BaseController
    {
        /// <summary>
        /// The braintree manager.
        /// </summary>
        private readonly IBraintreeManager braintreeManager;

        /// <summary>
        /// The rules engine service.
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// The payment provider factory.
        /// </summary>
        private readonly IPaymentProviderFactory paymentProviderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="braintreeManager">The braintree manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <param name="paymentProviderFactory">The payment provider factory.</param>
        /// <inheritdoc />
        public BraintreeController(
            ILoggingService loggingService,
            IBraintreeManager braintreeManager,
            IRulesEngineService rulesEngineService,
            IPaymentProviderFactory paymentProviderFactory)
            : base(loggingService)
        {
            this.braintreeManager = braintreeManager;
            this.rulesEngineService = rulesEngineService;
            this.paymentProviderFactory = paymentProviderFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.TransactionsController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="braintreeTransactionsManager">The transactions manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public BraintreeController(
            UmbracoContext umbracoContext,
            ILoggingService loggingService,
            IBraintreeManager braintreeTransactionsManager,
            IRulesEngineService rulesEngineService)
            : base(loggingService, 
                   umbracoContext)
        {
            this.braintreeManager = braintreeTransactionsManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.TransactionsController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="braintreeTransactionsManager">The transactions manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public BraintreeController(
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ILoggingService loggingService,
            IBraintreeManager braintreeTransactionsManager,
            IRulesEngineService rulesEngineService)
            : base(loggingService, 
                   umbracoContext, 
                   umbracoHelper)
        {
            this.braintreeManager = braintreeTransactionsManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Gets the transactions page.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetTransactionsPage()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                string partialView = paymentProviderFactory.GetTransactionsPartialView(
                    UmbracoContext,
                    false);

                return PartialView(partialView);
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Gets the braintree transactions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetTransactions()
        {
            LoggingService.Info(GetType(), string.Empty);

            IEnumerable<BraintreeTransactionViewModel> viewModels = braintreeManager.GetTransactionsViewModel(UmbracoContext);

             return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the braintree boot grid transactions.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBootGridTransactions(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems)
        {
            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                BootGridViewModel<BraintreeTransactionViewModel> bootGridViewModel = braintreeManager.GetBootGridTransactions(
                    current,
                    rowCount,
                    searchPhrase,
                    sortItems,
                    UmbracoContext);

                string jsonString = JsonConvert.SerializeObject(
                    bootGridViewModel,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()

                    });

                return Content(jsonString, "application/json");
            }

            return Content(string.Empty, "application/json");
        }
        
        /// <summary>
        /// Views the braintree transaction.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult ViewTransaction(string id)
        {
            LoggingService.Info(GetType(), "Id=" + id);

            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                string partialView = paymentProviderFactory
                                        .GetTransactionPartialView(UmbracoContext, false);
                
                BraintreeTransactionViewModel viewModel = braintreeManager.GetTransactionViewModel(UmbracoContext, id);

                return PartialView(partialView, viewModel);
            }

            return default(PartialViewResult);
        }
    }
}
