namespace Spectrum.Content.Payments.Controllers
{
    using Providers;
    using Translators;
    using Content.Services;
    using System.Web.Mvc;
    using ContentModels;
    using ViewModels;

    public class TransactionsController : BaseController
    {
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
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="transactionsTranslator">The transactions translator.</param>
        public TransactionsController(
            ILoggingService loggingService, 
            IPaymentProvider paymentProvider, 
            ITransactionsTranslator transactionsTranslator) 
            : base(loggingService)
        {
            this.paymentProvider = paymentProvider;
            this.transactionsTranslator = transactionsTranslator;
        }

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetTransactions()
        {
            BraintreeModel model = paymentProvider.GetBraintreeModel(UmbracoContext);

            TransactionsViewModel viewModel = transactionsTranslator.Translate(paymentProvider.GetTransactions(model));

            return PartialView("Partials/Spectrum/Payments/TransactionList", viewModel);
        }
    }
}
