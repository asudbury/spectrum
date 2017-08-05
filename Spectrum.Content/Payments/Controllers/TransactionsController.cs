namespace Spectrum.Content.Payments.Controllers
{
    using Content.Services;
    using Managers;
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
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="transactionsManager">The transactions manager.</param>
        public TransactionsController(
            ILoggingService loggingService,
            ITransactionsManager transactionsManager) 
            : base(loggingService)
        {
            this.transactionsManager = transactionsManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="transactionsManager">The transactions manager.</param>
        public TransactionsController(
            UmbracoContext umbracoContext,
            ILoggingService loggingService,
            ITransactionsManager transactionsManager)
            : base(loggingService, 
                   umbracoContext)
        {
            this.transactionsManager = transactionsManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsController"/> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="transactionsManager">The transactions manager.</param>
        public TransactionsController(
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ILoggingService loggingService,
            ITransactionsManager transactionsManager)
            : base(loggingService, 
                   umbracoContext, 
                   umbracoHelper)
        {
            this.transactionsManager = transactionsManager;
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
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBootGridTransactions(
            int current,
            int rowCount,
            string searchPhrase)
        {
            BootGridViewModel<TransactionViewModel> bootGridViewModel = transactionsManager.GetBootGridTransactions(
                current,
                rowCount,
                searchPhrase,
                UmbracoContext);

            string jsonString = JsonConvert.SerializeObject(
                bootGridViewModel,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()

                });

            return Content(jsonString, "application/json");
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
