namespace Spectrum.Content.Invoices.Managers
{
    using Content.Models;
    using ContentModels;
    using Customer.Providers;
    using Customer.Services;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Translators;
    using Umbraco.Web;
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
        /// The invoices boot grid translator.
        /// </summary>
        private readonly IInvoicesBootGridTranslator invoicesBootGridTranslator;

        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceManager" /> class.
        /// </summary>
        /// <param name="invoiceTranslator">The invoice translator.</param>
        /// <param name="invoiceService">The invoice service.</param>
        /// <param name="clientService">The client service.</param>
        /// <param name="postalAddressService">The postal address service.</param>
        /// <param name="invoicesBootGridTranslator">The invoices boot grid translator.</param>
        /// <param name="customerProvider">The customer provider.</param>
        public InvoiceManager(
            IInvoiceTranslator invoiceTranslator,
            IInvoiceService invoiceService,
            IClientService clientService,
            IPostalAddressService postalAddressService,
            IInvoicesBootGridTranslator invoicesBootGridTranslator,
            ICustomerProvider customerProvider)
        {
            this.invoiceTranslator = invoiceTranslator;
            this.invoiceService = invoiceService;
            this.clientService = clientService;
            this.postalAddressService = postalAddressService;
            this.invoicesBootGridTranslator = invoicesBootGridTranslator;
            this.customerProvider = customerProvider;
        }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public void CreateInvoice(CreateInvoiceViewModel viewModel)
        {
            InvoiceModel invoiceModel = invoiceTranslator.Translate(viewModel);

            int addressId = postalAddressService.GetAddressId(
                                invoiceModel.CustomerId,
                                "",
                                "",
                                "");

            int clientId = clientService.GetClientId(
                                    invoiceModel.CustomerId, 
                                    addressId,
                                    viewModel.ClientName, 
                                    viewModel.EmailAddress);

            invoiceModel.ClientId = clientId;
            invoiceModel.AddressId = addressId;

            invoiceService.CreateInvoice(invoiceModel);
        }

        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        public IEnumerable<InvoiceViewModel> GetInvoices(
            UmbracoContext umbracoContext, 
            DateTime dateRangeStart, 
            DateTime dateRangeEnd)
        {
            IEnumerable<InvoiceModel> models =  invoiceService.GetInvoices(
                                                    dateRangeStart, 
                                                    dateRangeEnd, GetCustomerId(umbracoContext));

            List<InvoiceViewModel> viewModels = new List<InvoiceViewModel>();

            foreach (InvoiceModel model in models)
            {
                viewModels.Add(invoiceTranslator.Translate(model));
            }

            return viewModels;
        }

        /// <summary>
        /// Gets the boot grid invoices.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        public string GetBootGridInvoices(
            int current, 
            int rowCount, 
            string searchPhrase, 
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext, 
            DateTime dateRangeStart, 
            DateTime dateRangeEnd)
        {
            IEnumerable<InvoiceViewModel> viewModels = GetInvoices(
                umbracoContext,
                dateRangeStart,
                dateRangeEnd);

            BootGridViewModel<InvoiceViewModel> bootGridViewModel = invoicesBootGridTranslator.Translate(
                viewModels.ToList(),
                current,
                rowCount,
                searchPhrase,
                sortItems);

            string jsonString = JsonConvert.SerializeObject(
                bootGridViewModel,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            return jsonString;
        }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        internal int GetCustomerId(UmbracoContext context)
        {
            CustomerModel customerModel = customerProvider.GetCustomerModel(context);
            return customerModel.Id;
        }
    }
}
