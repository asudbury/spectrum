using Spectrum.Content.Customer.Providers;

namespace Spectrum.Content.Payments.Managers
{
    using Autofac.Events;
    using Braintree;
    using Content.Services;
    using ContentModels;
    using Messages;
    using Providers;
    using Repositories;
    using System;
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
        /// Initializes a new instance of the <see cref="PaymentManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="transactionsRepository">The transactions repository.</param>
        public PaymentManager(
            ILoggingService loggingService,
            ICustomerProvider customerProvider,
            IPaymentProvider paymentProvider,
            IEventPublisher eventPublisher,
            ITransactionsRepository transactionsRepository)
        {
            this.loggingService = loggingService;
            this.customerProvider = customerProvider;
            this.paymentProvider = paymentProvider;
            this.eventPublisher = eventPublisher;
            this.transactionsRepository = transactionsRepository;
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