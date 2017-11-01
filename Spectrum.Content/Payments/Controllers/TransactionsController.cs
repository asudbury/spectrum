namespace Spectrum.Content.Payments.Controllers
{
    using Content.Services;
    using Managers;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Umbraco.Web;
    using ViewModels;

    public class TransactionsController : BaseController
    {
        /// <summary>
        /// The transactions manager.
        /// </summary>
        private readonly ITransactionsManager transactionsManager;

        /// <summary>
        /// The rules engine service.
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="transactionsManager">The transactions manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public TransactionsController(
            ILoggingService loggingService,
            ITransactionsManager transactionsManager,
            IRulesEngineService rulesEngineService) 
            : base(loggingService)
        {
            this.transactionsManager = transactionsManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.TransactionsController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="transactionsManager">The transactions manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public TransactionsController(
            UmbracoContext umbracoContext,
            ILoggingService loggingService,
            ITransactionsManager transactionsManager,
            IRulesEngineService rulesEngineService)
            : base(loggingService, 
                   umbracoContext)
        {
            this.transactionsManager = transactionsManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.TransactionsController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="transactionsManager">The transactions manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public TransactionsController(
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ILoggingService loggingService,
            ITransactionsManager transactionsManager,
            IRulesEngineService rulesEngineService)
            : base(loggingService, 
                   umbracoContext, 
                   umbracoHelper)
        {
            this.transactionsManager = transactionsManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Transactionses this instance.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Transactions()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                return PartialView("");
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetTransactions()
        {
            LoggingService.Info(GetType(), string.Empty);

            IEnumerable<TransactionViewModel> viewModels = transactionsManager.GetTransactionsViewModel(UmbracoContext);

             return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the boot grid transactions.
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
                BootGridViewModel<TransactionViewModel> bootGridViewModel = transactionsManager.GetBootGridTransactions(
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
        /// Views the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult View(string id)
        {
            LoggingService.Info(GetType(), "Id=" + id);

            TransactionViewModel viewModel = transactionsManager.GetTransactionViewModel(UmbracoContext, id);

            return PartialView("Partials/Spectrum/Payments/Transaction", viewModel);
        }
    }
}
