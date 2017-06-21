namespace Spectrum.Content.Payments.Controllers
{
    using Providers;
    using Translators;
    using Content.Services;
    using System.Web.Mvc;
    using Umbraco.Web;
    using ContentModels;
    using ViewModels;

    public class TransactionsController : BaseController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The payment provider.
        /// </summary>
        private readonly IPaymentProvider paymentProvider;

        /// <summary>
        /// The transactions translator.
        /// </summary>
        private readonly ITransactionsTranslator transactionsTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="transactionsTranslator">The transactions translator.</param>
        public TransactionsController(
            ILoggingService loggingService, 
            ISettingsService settingsService, 
            IPaymentProvider paymentProvider, 
            ITransactionsTranslator transactionsTranslator) 
            : base(loggingService)
        {
            this.settingsService = settingsService;
            this.paymentProvider = paymentProvider;
            this.transactionsTranslator = transactionsTranslator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="transactionsTranslator">The transactions translator.</param>
        public TransactionsController(
            UmbracoContext context, 
            ILoggingService loggingService, 
            ISettingsService settingsService, 
            IPaymentProvider paymentProvider, 
            ITransactionsTranslator transactionsTranslator) 
            : base(context, loggingService)
        {
            this.settingsService = settingsService;
            this.paymentProvider = paymentProvider;
            this.transactionsTranslator = transactionsTranslator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsController"/> class.
        /// </summary>
        public TransactionsController()
            : this(new LoggingService(), 
                   new SettingsService(),
                   new PaymentProvider(), 
                   new TransactionsTranslator())
        {
        }

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetTransactions()
        {
            BraintreeModel model = paymentProvider.GetBraintreeModel(settingsService.GetPaymentsNode(UmbracoContext));

            TransactionsViewModel viewModel = transactionsTranslator.Translate(paymentProvider.GetTransactions(model));

            return PartialView("Partials/Spectrum/Payments/TransactionList", viewModel);
        }
    }
}
