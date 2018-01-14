﻿namespace Spectrum.Content.Payments.Managers
{
    using Content.Services;
    using ContentModels;
    using Customer.Providers;
    using Mail.Providers;
    using Messages;
    using Models;
    using Providers;
    using System.Collections.Generic;
    using System.Globalization;
    using Translators.Interfaces;

    public class PaymentMadeManager : IPaymentMadeManager
    {
        /// <summary>
        /// The logging service.
        /// </summary>
        private readonly ILoggingService loggingService;

        /// <summary>
        /// The database provider.
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// The payment translator
        /// </summary>
        private readonly IPaymentTranslator paymentTranslator;

        /// <summary>
        /// The mail provider.
        /// </summary>
        private readonly IMailProvider mailProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMadeManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="databaseProvider">The database provider.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="paymentTranslator">The payment translator.</param>
        /// <param name="mailProvider">The mail provider.</param>
        public PaymentMadeManager(
            ILoggingService loggingService,
            IDatabaseProvider databaseProvider,
            ICustomerProvider customerProvider,
            IPaymentTranslator paymentTranslator,
            IMailProvider mailProvider)
        {
            this.loggingService = loggingService;
            this.databaseProvider = databaseProvider;
            this.customerProvider = customerProvider;
            this.paymentTranslator = paymentTranslator;
            this.mailProvider = mailProvider;
        }

        /// <inheritdoc />
        /// <summary>
        /// Handles the specified transaction made message.
        /// </summary>
        /// <param name="paymentMadeMessage">The payment made message.</param>
        public void Handle(TransactionMadeMessage paymentMadeMessage)
        {
            string transactionId = paymentMadeMessage.Transaction.Id;

            string message = "TransactionMadeMessage " + "TransactionId=" + transactionId + " ";

            loggingService.Info(GetType(), message);

            CustomerModel customerModel = customerProvider.GetCustomerModel(paymentMadeMessage.UmbracoContext);

            TransactionModel model = paymentTranslator.Translate(paymentMadeMessage);
            model.CustomerId = customerModel.Id;

            databaseProvider.InsertTransaction(model);

            ///// do we want to send a confirmation email??
           
            if (string.IsNullOrEmpty(paymentMadeMessage.EmailTemplateName) == false &&
                string.IsNullOrEmpty(paymentMadeMessage.ClientViewModel.EmailAddress) == false)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    {"PaymentId", transactionId},
                    {"InvoiceId", paymentMadeMessage.PaymentViewModel.InvoiceId},
                    {"PaymentAmount", paymentMadeMessage.PaymentViewModel.Amount.ToString(CultureInfo.InvariantCulture)},
                };

                loggingService.Info(GetType(), "Sending Email");

                mailProvider.SendEmail(
                    paymentMadeMessage.UmbracoContext,
                    paymentMadeMessage.EmailTemplateName,
                    paymentMadeMessage.ClientViewModel.EmailAddress,
                    null,
                    dictionary);
            }
        }
    }
}
