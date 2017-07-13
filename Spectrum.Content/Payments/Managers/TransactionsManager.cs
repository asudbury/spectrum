using System.Linq;
using Braintree;

namespace Spectrum.Content.Payments.Managers
{
    using Content.Services;
    using ContentModels;
    using Providers;
    using System.Collections.Generic;
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
        /// The transaction translator.
        /// </summary>
        private readonly ITransactionTranslator transactionTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="transactionTranslator">The transaction translator.</param>
        public TransactionsManager(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            ITransactionTranslator transactionTranslator)
        {
            this.loggingService = loggingService;
            this.paymentProvider = paymentProvider;
            this.transactionTranslator = transactionTranslator;
        }

        /// <summary>
        /// Gets the transactions view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public IEnumerable<TransactionViewModel> GetTransactionsViewModel(UmbracoContext umbracoContext)
        {
            loggingService.Info(GetType(), string.Empty);

            BraintreeModel model = paymentProvider.GetBraintreeModel(umbracoContext);

            ResourceCollection<Transaction> transactions = paymentProvider.GetTransactions(model);

            List <TransactionViewModel> viewModels = (from Transaction transaction 
                                                      in transactions
                                                      select transactionTranslator.Translate(transaction))
                                                      .ToList();

            return viewModels;
        }
    }
}