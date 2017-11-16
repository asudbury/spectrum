namespace Spectrum.Content.Payments.Managers
{
    using Braintree;
    using Content.Services;
    using ContentModels;
    using Content.Models;
    using Providers;
    using Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using Translators.Interfaces;
    using Umbraco.Web;
    using ViewModels;

    public class BraintreeManager : IBraintreeManager
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
        private readonly IBraintreeTransactionTranslator transactionTranslator;

        /// <summary>
        /// The transactions repository.
        /// </summary>
        private readonly ITransactionsRepository transactionsRepository;

        /// <summary>
        /// The transactions boot grid translator.
        /// </summary>
        private readonly ITransactionsBootGridTranslator transactionsBootGridTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BraintreeManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="transactionTranslator">The transaction translator.</param>
        /// <param name="transactionsRepository">The transactions repository.</param>
        /// <param name="transactionsBootGridTranslator">The transactions boot grid translator.</param>
        public BraintreeManager(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            IBraintreeTransactionTranslator transactionTranslator,
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
        public IEnumerable<BraintreeTransactionViewModel> GetTransactionsViewModel(UmbracoContext umbracoContext)
        {
            loggingService.Info(GetType());

            List<BraintreeTransactionViewModel> viewModels = new List<BraintreeTransactionViewModel>();

            PaymentSettingsModel model = paymentProvider.GetPaymentSettingsModel(umbracoContext);

            if (model.PaymentsEnabled)
            {
                transactionsRepository.SetKey(umbracoContext);

                bool exists = transactionsRepository.Exists();

                if (exists)
                {
                    return transactionsRepository.Get<IEnumerable<BraintreeTransactionViewModel>>();
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
        public BraintreeTransactionViewModel GetTransactionViewModel(
            UmbracoContext umbracoContext,
            string transactionId)
        {
            loggingService.Info(GetType(), "EncryptedTransactionId=" + transactionId);

            PaymentSettingsModel model = paymentProvider.GetPaymentSettingsModel(umbracoContext);

            transactionsRepository.SetKey(umbracoContext);

            bool exists = transactionsRepository.Exists();

            if (exists)
            {
                IEnumerable<BraintreeTransactionViewModel> viewModels = transactionsRepository.Get<IEnumerable<BraintreeTransactionViewModel>>();

                BraintreeTransactionViewModel viewModel = viewModels.FirstOrDefault(x => x.Id == transactionId);

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
        public BootGridViewModel<BraintreeTransactionViewModel> GetBootGridTransactions(
            int current, 
            int rowCount, 
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext)
        {
            IEnumerable<BraintreeTransactionViewModel> viewModels = GetTransactionsViewModel(umbracoContext);

            return transactionsBootGridTranslator.Translate(
                viewModels.ToList(), 
                current, 
                rowCount, 
                searchPhrase, 
                sortItems);
        }
    }
}