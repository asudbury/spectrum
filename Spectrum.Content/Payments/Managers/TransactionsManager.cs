namespace Spectrum.Content.Payments.Managers
{
    using Braintree;
    using Content.Services;
    using ContentModels;
    using Models;
    using Providers;
    using Repositories;
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
        /// The transactions repository.
        /// </summary>
        private readonly ITransactionsRepository transactionsRepository;

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
        /// <param name="transactionsRepository">The transactions repository.</param>
        /// <param name="transactionsBootGridTranslator">The transactions boot grid translator.</param>
        public TransactionsManager(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            ITransactionTranslator transactionTranslator,
            ITransactionsRepository transactionsRepository,
            ITransactionsBootGridTranslator transactionsBootGridTranslator)
        {
            this.loggingService = loggingService;
            this.paymentProvider = paymentProvider;
            this.transactionTranslator = transactionTranslator;
            this.transactionsRepository = transactionsRepository;
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

            List<TransactionViewModel> viewModels = new List<TransactionViewModel>();

            PaymentSettingsModel model = paymentProvider.GetBraintreeModel(umbracoContext);

            if (model.PaymentsEnabled)
            {
                transactionsRepository.SetKey(umbracoContext);

                bool exists = transactionsRepository.Exists();

                if (exists)
                {
                    return transactionsRepository.Get<IEnumerable<TransactionViewModel>>();
                }

                ResourceCollection<Transaction> transactions = paymentProvider.GetTransactions(model);

                viewModels = (from Transaction transaction
                            in transactions
                            select transactionTranslator.Translate(transaction))
                            .ToList();

                transactionsRepository.Add(viewModels);
            }

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

            PaymentSettingsModel model = paymentProvider.GetBraintreeModel(umbracoContext);

            transactionsRepository.SetKey(umbracoContext);

            bool exists = transactionsRepository.Exists();

            if (exists)
            {
                IEnumerable<TransactionViewModel> viewModels = transactionsRepository.Get<IEnumerable<TransactionViewModel>>();

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