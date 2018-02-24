namespace Spectrum.Content.Invoices.Managers
{
    using Models;
    using Content.Services;
    using ContentModels;
    using Customer.Providers;
    using Messages;
    using Scorchio.Services;
    using Services;

    public class PaymentMadeManager : IPaymentMadeManager
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
        /// The invoice service.
        /// </summary>
        private readonly IInvoiceService invoiceService;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMadeManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="invoiceService">The invoice service.</param>
        /// <param name="encryptionService">The encryption service.</param>
        public PaymentMadeManager(
            ILoggingService loggingService,
            ICustomerProvider customerProvider,
            IInvoiceService invoiceService,
            IEncryptionService encryptionService)
        {
            this.loggingService = loggingService;
            this.customerProvider = customerProvider;
            this.invoiceService = invoiceService;
            this.encryptionService = encryptionService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Handles the specified payment made message.
        /// </summary>
        /// <param name="paymentMadeMessage">The payment made message.</param>
        public void Handle(TransactionMadeMessage paymentMadeMessage)
        {
            string paymentId = paymentMadeMessage.Transaction.Id;
            string invoiceId = encryptionService.DecryptString(paymentMadeMessage.PaymentViewModel.InvoiceId);

            string message = "PaymentMadeMessage " +
                             "PaymentId=" + paymentId + " " +
                             "InvoiceId=" + invoiceId + " "; 

            loggingService.Info(GetType(), message);

            CustomerModel customerModel = customerProvider.GetCustomerModel(paymentMadeMessage.UmbracoContext);

            int customerId = customerModel.Id;

            UpdateInvoice(invoiceId, customerId, paymentId);
        }

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="paymentId">The payment identifier.</param>
        internal void UpdateInvoice(
            string invoiceId,
            int customerId,
            string paymentId)
        {
            InvoiceModel model = invoiceService.GetInvoice(customerId, invoiceId);

            if (model != null)
            {
                loggingService.Info(GetType(), "Invoice Found");

                model.PaymentId = paymentId;

                invoiceService.UpdateInvoice(model);

                loggingService.Info(GetType(), "Invoice updated with PaymentId=" + paymentId);
            }
        }
    }
}
