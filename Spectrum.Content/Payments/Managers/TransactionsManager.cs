namespace Spectrum.Content.Payments.Managers
{
    using Application.Services;
    using Braintree;
    using Content.Services;
    using ContentModels;
    using Models;
    using Providers;
    using System.Collections.Generic;
    using System.Linq;
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
        /// The cache service.
        /// </summary>
        private readonly ICacheService cacheService;

        /// <summary>
        /// The transactions boot grid translator.
        /// </summary>
        private readonly ITransactionsBootGridTranslator transactionsBootGridTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="transactionTranslator">The transaction translator.</param>
        /// <param name="cacheService">The cache service.</param>
        /// <param name="transactionsBootGridTranslator">The transactions boot grid translator.</param>
        public TransactionsManager(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            ITransactionTranslator transactionTranslator,
            ICacheService cacheService,
            ITransactionsBootGridTranslator transactionsBootGridTranslator)
        {
            this.loggingService = loggingService;
            this.paymentProvider = paymentProvider;
            this.transactionTranslator = transactionTranslator;
            this.cacheService = cacheService;
            this.transactionsBootGridTranslator = transactionsBootGridTranslator;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the transactions view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public IEnumerable<TransactionViewModel> GetTransactionsViewModel(UmbracoContext umbracoContext)
        {
            loggingService.Info(GetType());

            bool exists = cacheService.Exists("Transactions");

            if (exists)
            {
                return cacheService.Get<IEnumerable<TransactionViewModel>>("Transactions");
            }

            BraintreeSettingsModel model = paymentProvider.GetBraintreeModel(umbracoContext);


            ResourceCollection<Transaction> transactions = paymentProvider.GetTransactions(model);

            List <TransactionViewModel> viewModels = (from Transaction transaction 
                                                      in transactions
                                                      select transactionTranslator.Translate(transaction))
                                                      .ToList();

            cacheService.Add(viewModels, "Transactions");

            return viewModels;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        public TransactionViewModel GetTransactionViewModel(
            UmbracoContext umbracoContext,
            string transactionId)
        {
            loggingService.Info(GetType(), "EncryptedTransactionId=" + transactionId);

            BraintreeSettingsModel model = paymentProvider.GetBraintreeModel(umbracoContext);

            bool exists = cacheService.Exists("Transactions");

            if (exists)
            {
                IEnumerable<TransactionViewModel> viewModels = cacheService.Get<IEnumerable<TransactionViewModel>>("Transactions");

                TransactionViewModel viewModel = viewModels.FirstOrDefault(x => x.Id == transactionId);

                if (viewModel != null)
                {
                    return viewModel;
                }
            }

            Transaction transaction = paymentProvider.GetTransaction(model, transactionId);

            return transactionTranslator.Translate(transaction);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the boot grid transactions.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems"></param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public BootGridViewModel<TransactionViewModel> GetBootGridTransactions(
            int current, 
            int rowCount, 
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext)
        {
            IEnumerable<TransactionViewModel> viewModels = GetTransactionsViewModel(umbracoContext);

            return transactionsBootGridTranslator.Translate(
                viewModels.ToList(), 
                current, 
                rowCount, 
                searchPhrase, 
                sortItems);
        }
    }

}