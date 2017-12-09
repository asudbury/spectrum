namespace Spectrum.Content.Invoices.Managers
{
    using Customer.Services;
    using Models;
    using Services;
    using Translators;
    using ViewModels;

    public class InvoiceManager : IInvoiceManager
    {
        /// <summary>
        /// The invoice translator.
        /// </summary>
        private readonly IInvoiceTranslator invoiceTranslator;

        /// <summary>
        /// The invoice service.
        /// </summary>
        private readonly IInvoiceService invoiceService;

        /// <summary>
        /// The client service.
        /// </summary>
        private readonly IClientService clientService;

        /// <summary>
        /// The postal address service.
        /// </summary>
        private readonly IPostalAddressService postalAddressService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceManager" /> class.
        /// </summary>
        /// <param name="invoiceTranslator">The invoice translator.</param>
        /// <param name="invoiceService">The invoice service.</param>
        /// <param name="clientService">The client service.</param>
        /// <param name="postalAddressService">The postal address service.</param>
        public InvoiceManager(
            IInvoiceTranslator invoiceTranslator,
            IInvoiceService invoiceService,
            IClientService clientService,
            IPostalAddressService postalAddressService)
        {
            this.invoiceTranslator = invoiceTranslator;
            this.invoiceService = invoiceService;
            this.clientService = clientService;
            this.postalAddressService = postalAddressService;
        }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public void CreateInvoice(CreateInvoiceViewModel viewModel)
        {
            InvoiceModel invoiceModel = invoiceTranslator.Translate(viewModel);

            int clientId = clientService.GetClientId(
                                    invoiceModel.CustomerId, 
                                    viewModel.ClientName, 
                                    viewModel.EmailAddress);

            int addressId = postalAddressService.GetAddressId(
                                    invoiceModel.CustomerId, 
                                    "", 
                                    "",
                                    "");

            invoiceModel.ClientId = clientId;
            invoiceModel.AddressId = addressId;

            invoiceService.CreateInvoice(invoiceModel);
        }
    }
}
