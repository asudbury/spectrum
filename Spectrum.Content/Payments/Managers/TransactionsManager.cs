namespace Spectrum.Content.Payments.Managers
{
    using Content.Services;
    using ContentModels;
    using Providers;
    using Translators;
    using Umbraco.Web;
    using ViewModels;
    
    public class TransactionsManager : ITransactionsManager
    {
        /// <summary>
        /// The logging service.
        /// </summary>
        private readonly ILoggingService loggingService;

        /// <summary>
        /// The payment provider.
        /// </summary>
        private readonly IPaymentProvider paymentProvider;

        /// <summary>
        /// The transactions translator.
        /// </summary>
        private readonly ITransactionsTranslator transactionsTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="transactionsTranslator">The transactions translator.</param>
        public TransactionsManager(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            ITransactionsTranslator transactionsTranslator)
        {
            this.loggingService = loggingService;
            this.paymentProvider = paymentProvider;
            this.transactionsTranslator = transactionsTranslator;
        }

        /// <summary>
        /// Gets the transactions view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public TransactionsViewModel GetTransactionsViewModel(UmbracoContext umbracoContext)
        {
            loggingService.Info(GetType(), string.Empty);

            BraintreeModel model = paymentProvider.GetBraintreeModel(umbracoContext);

            return transactionsTranslator.Translate(paymentProvider.GetTransactions(model));
        }
    }
}