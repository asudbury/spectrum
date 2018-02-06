using Spectrum.Application.Services;

namespace Spectrum.Content.Payments.Managers
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
        /// The token service.
        /// </summary>
        private readonly ITokenService tokenService;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMadeManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="databaseProvider">The database provider.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="paymentTranslator">The payment translator.</param>
        /// <param name="mailProvider">The mail provider.</param>
        /// <param name="tokenService">The token service.</param>
        /// <param name="encryptionService">The encryption service.</param>
        public PaymentMadeManager(
            ILoggingService loggingService,
            IDatabaseProvider databaseProvider,
            ICustomerProvider customerProvider,
            IPaymentTranslator paymentTranslator,
            IMailProvider mailProvider,
            ITokenService tokenService,
            IEncryptionService encryptionService)
        {
            this.loggingService = loggingService;
            this.databaseProvider = databaseProvider;
            this.customerProvider = customerProvider;
            this.paymentTranslator = paymentTranslator;
            this.mailProvider = mailProvider;
            this.tokenService = tokenService;
            this.encryptionService = encryptionService;
        }

        /// <summary>
        /// Handles the specified transaction made message.
        /// </summary>
        /// <param name="transactionMadeMessage">The transaction made message.</param>
        /// <inheritdoc />
        public void Handle(TransactionMadeMessage transactionMadeMessage)
        {
            string transactionId = transactionMadeMessage.Transaction.Id;

            string message = "TransactionMadeMessage " + "TransactionId=" + transactionId + " ";

            loggingService.Info(GetType(), message);

            CustomerModel customerModel = customerProvider.GetCustomerModel(transactionMadeMessage.UmbracoContext);

            TransactionModel model = paymentTranslator.Translate(transactionMadeMessage);
            model.CustomerId = customerModel.Id;

            databaseProvider.InsertTransaction(model);

            ///// do we want to send a confirmation email??
           
            if (string.IsNullOrEmpty(transactionMadeMessage.EmailTemplateName) == false &&
                string.IsNullOrEmpty(transactionMadeMessage.ClientViewModel.EmailAddress) == false)
            {
                Dictionary<string, string> dictionary = tokenService.GetBaseTokens(
                                                            customerModel,
                                                            transactionMadeMessage.ClientViewModel.Name);

                string invoiceId = encryptionService.DecryptString(transactionMadeMessage.PaymentViewModel.InvoiceId);

                dictionary.Add("PaymentId", transactionId);
                dictionary.Add("InvoiceId", invoiceId);
                dictionary.Add("PaymentAmount", transactionMadeMessage.PaymentViewModel.Amount.ToString(CultureInfo.InvariantCulture));

                loggingService.Info(GetType(), "Sending " + transactionMadeMessage.EmailTemplateName + " Email");
                
                mailProvider.SendEmail(
                    transactionMadeMessage.UmbracoContext,
                    transactionMadeMessage.EmailTemplateName,
                    customerModel.EmailAddress,
                    transactionMadeMessage.ClientViewModel.EmailAddress,
                    string.Empty,
                    null,
                    dictionary);
            }
        }
    }
}
