namespace Spectrum.Content.Payments.Managers
{
    using Autofac.Events;
    using Braintree;
    using Content.Services;
    using ContentModels;
    using Mail.Models;
    using Mail.Providers;
    using Messages;
    using Providers;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
        /// The mail provider.
        /// </summary>
        private readonly IMailProvider mailProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="transactionsRepository">The transactions repository.</param>
        /// <param name="mailProvider">The mail provider.</param>
        public PaymentManager(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            IEventPublisher eventPublisher,
            ITransactionsRepository transactionsRepository,
            IMailProvider mailProvider)
        {
            this.loggingService = loggingService;
            this.paymentProvider = paymentProvider;
            this.eventPublisher = eventPublisher;
            this.transactionsRepository = transactionsRepository;
            this.mailProvider = mailProvider;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public string GetAuthToken(UmbracoContext umbracoContext)
        {
            PaymentSettingsModel model = paymentProvider.GetBraintreeModel(umbracoContext);

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
            PaymentSettingsModel model = paymentProvider.GetBraintreeModel(umbracoContext);

            return model?.Environment;
        }

        /// <inheritdoc />
        /// <summary>
        /// Handles the payment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        /// <exception cref="T:System.ApplicationException">
        /// Current Page Id Not Set
        /// Next Page Url Not Set
        /// Error Page Url Not Set
        /// </exception>
        public string HandlePayment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            PaymentViewModel viewModel)
        {
            loggingService.Info(GetType(), "HandlePayment MakePayment");

            PageModel pageModel = new PageModel(publishedContent);

            if (string.IsNullOrEmpty(pageModel.NextPageUrl))
            {
                throw new ApplicationException("Next Page Url Not Set");
            }

            if (string.IsNullOrEmpty(pageModel.ErrorPageUrl))
            {
                throw new ApplicationException("Error Page Url Not Set");
            }

            PaymentSettingsModel model = paymentProvider.GetBraintreeModel(umbracoContext);

            Result<Transaction> transaction = paymentProvider.MakePayment(model, viewModel);

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
                    eventPublisher.Publish(new PaymentMadeMessage(
                        umbracoContext,
                        transaction,
                        viewModel.AutoAllocate,
                        viewModel.AppointmentId));
                }
                catch (Exception exception)
                {
                    loggingService.Error(GetType(), "Publish of payment Failed", exception);
                }

                try
                {
                    if (string.IsNullOrEmpty(pageModel.EmailTemplateName) == false &&
                        string.IsNullOrEmpty(viewModel.EmailAddress) == false)
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>
                        {
                            {"PaymentId", paymentId},
                            {"AppointmentId", viewModel.AppointmentId},
                            {"PaymentAmount", viewModel.Amount.ToString(CultureInfo.InvariantCulture)},
                        };

                        loggingService.Info(GetType(), "Sending Email");

                        MailResponse mailResponse = mailProvider.SendEmail(
                            umbracoContext,
                            pageModel.EmailTemplateName,
                            viewModel.EmailAddress,
                            null,
                            dictionary);

                        //// TODO : we need to log the mail response!
                    }
                }
                catch (Exception exception)
                {
                    loggingService.Error(GetType(), "Sending of confirmation email Failed", exception);
                }
                
                return pageModel.NextPageUrl;
            }

            loggingService.Info(GetType(), "Payment Failed");

            return pageModel.ErrorPageUrl;
        }
    }
}