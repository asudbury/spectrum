namespace Spectrum.Content.Payments.Managers
{
    using Application.Services;
    using Autofac.Events;
    using Braintree;
    using Content.Models;
    using Content.Services;
    using ContentModels;
    using Customer.Providers;
    using Messages;
    using Models;
    using Providers;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Translators.Interfaces;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class PaymentManager : IPaymentManager
    {
        /// <summary>
        /// The logging service.
        /// </summary>
        private readonly ILoggingService loggingService;

        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// The payment provider.
        /// </summary>
        private readonly IPaymentProvider paymentProvider;

        /// <summary>
        /// The event publisher.
        /// </summary>
        private readonly IEventPublisher eventPublisher;

        /// <summary>
        /// The transactions repository.
        /// </summary>
        private readonly ITransactionsRepository transactionsRepository;

        /// <summary>
        /// The transaction translator.
        /// </summary>
        private readonly ITransactionTranslator transactionTranslator;

        /// <summary>
        /// The database provider.
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        /// <summary>
        /// The transactions boot grid translator.
        /// </summary>
        private readonly ITransactionsBootGridTranslator transactionsBootGridTranslator;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="transactionsRepository">The transactions repository.</param>
        /// <param name="transactionTranslator">The transaction translator.</param>
        /// <param name="databaseProvider">The database provider.</param>
        /// <param name="transactionsBootGridTranslator">The transactions boot grid translator.</param>
        /// <param name="encryptionService">The encryption service.</param>
        public PaymentManager(
            ILoggingService loggingService,
            ICustomerProvider customerProvider,
            IPaymentProvider paymentProvider,
            IEventPublisher eventPublisher,
            ITransactionsRepository transactionsRepository,
            ITransactionTranslator transactionTranslator,
            IDatabaseProvider databaseProvider,
            ITransactionsBootGridTranslator transactionsBootGridTranslator,
            IEncryptionService encryptionService)
        {
            this.loggingService = loggingService;
            this.customerProvider = customerProvider;
            this.paymentProvider = paymentProvider;
            this.eventPublisher = eventPublisher;
            this.transactionsRepository = transactionsRepository;
            this.transactionTranslator = transactionTranslator;
            this.databaseProvider = databaseProvider;
            this.transactionsBootGridTranslator = transactionsBootGridTranslator;
            this.encryptionService = encryptionService;
        }

        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public string GetCustomerName(UmbracoContext umbracoContext)
        {
            CustomerModel customerModel = customerProvider.GetCustomerModel(umbracoContext);
            return customerModel.CustomerName;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public string GetAuthToken(UmbracoContext umbracoContext)
        {
            PaymentSettingsModel model = paymentProvider.GetPaymentSettingsModel(umbracoContext);

            return model != null ? paymentProvider.GetAuthToken(model) : string.Empty;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public string GetEnvironment(UmbracoContext umbracoContext)
        {
            PaymentSettingsModel model = paymentProvider.GetPaymentSettingsModel(umbracoContext);

            return model?.Environment;
        }

        /// <summary>
        /// Gets the transaction view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="encryptedId">The encrypted identifier.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public TransactionViewModel GetTransactionViewModel(
            UmbracoContext umbracoContext,
            string encryptedId)
        {
            PaymentSettingsModel settingsModel = paymentProvider.GetPaymentSettingsModel(umbracoContext);

            if (settingsModel.PaymentsEnabled)
            {
                string paymentId = encryptionService.DecryptString(encryptedId);

                CustomerModel customerModel = customerProvider.GetCustomerModel(umbracoContext);

                TransactionModel model = databaseProvider.GetTransaction(Convert.ToInt32(paymentId), customerModel.Id);

                return transactionTranslator.Translate(model);
            }

            return new TransactionViewModel();
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

            PaymentSettingsModel model = paymentProvider.GetPaymentSettingsModel(umbracoContext);

            if (model.PaymentsEnabled)
            {
                CustomerModel customerModel = customerProvider.GetCustomerModel(umbracoContext);
                
                //// TODO : change at some point!
                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MaxValue;
                
                IEnumerable<TransactionModel> transactions = databaseProvider.GetTransactions(
                                                                        startDate,
                                                                        endDate,
                                                                        customerModel.Id);

                viewModels = (from TransactionModel transaction
                            in transactions
                        select transactionTranslator.Translate(transaction))
                    .ToList();
            }

            return viewModels;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the boot grid transactions.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
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

        /// <summary>
        /// Handles the payment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public string MakePayment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            MakePaymentViewModel viewModel,
            string currentUser)
        {
            loggingService.Info(GetType());

            PageModel pageModel = new PageModel(publishedContent);

            if (string.IsNullOrEmpty(pageModel.NextPageUrl))
            {
                throw new ApplicationException("Next Page Url Not Set");
            }

            if (string.IsNullOrEmpty(pageModel.ErrorPageUrl))
            {
                throw new ApplicationException("Error Page Url Not Set");
            }

            PaymentSettingsModel paymentSettingsModel = paymentProvider.GetPaymentSettingsModel(umbracoContext);

            if (paymentSettingsModel.Provider != Constants.PaymentProviders.Braintree)
            {
                //// we currently only support Braintree
                throw new ApplicationException("Unsupported Payment Provider");
            }

            Result<Transaction> transaction = paymentProvider.MakePayment(paymentSettingsModel, viewModel);

            if (transaction != null)
            {
                //// at this point the payment has worked
                //// so need to be careful from here as to what we raise as errors etc.

                //// make sure we clear the cache!
                transactionsRepository.SetKey(umbracoContext);
                transactionsRepository.Clear();

                string paymentId = transaction.Target.Id;

                loggingService.Info(GetType(), "Payment Succesful Id=" + paymentId);

                try
                {
                    eventPublisher.Publish(new TransactionMadeMessage(
                        umbracoContext,
                        transaction.Target,
                        viewModel,
                        currentUser,
                        pageModel.EmailTemplateName,
                        paymentSettingsModel.Provider,
                        paymentSettingsModel.Environment));
                }
                catch (Exception exception)
                {
                    loggingService.Error(GetType(), "Publish of payment Failed", exception);
                }
                
                return pageModel.NextPageUrl;
            }

            loggingService.Info(GetType(), "Payment Failed");

            return pageModel.ErrorPageUrl;
        }
    }
}