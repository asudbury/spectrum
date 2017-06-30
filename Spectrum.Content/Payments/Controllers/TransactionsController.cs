namespace Spectrum.Content.Payments.Controllers
{
    using Content.Services;
    using Managers;
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
        [ChildActionOnly]
        public ActionResult GetTransactions()
        {
            LoggingService.Info(GetType(), string.Empty);

            TransactionsViewModel viewModel = transactionsManager.GetTransactionsViewModel(UmbracoContext);

            return PartialView("Partials/Spectrum/Payments/TransactionList", viewModel);
        }
    }
}
