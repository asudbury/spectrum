namespace Spectrum.Content.Invoices.Managers
{
    using Application.Services;
    using Content.Models;
    using ContentModels;
    using Customer.Providers;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Translators;
    using Umbraco.Core.Models;
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
        /// The invoices boot grid translator.
        /// </summary>
        private readonly IInvoicesBootGridTranslator invoicesBootGridTranslator;

        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceManager" /> class.
        /// </summary>
        /// <param name="invoiceTranslator">The invoice translator.</param>
        /// <param name="invoiceService">The invoice service.</param>
        /// <param name="invoicesBootGridTranslator">The invoices boot grid translator.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="encryptionService">The encryption service.</param>
        public InvoiceManager(
            IInvoiceTranslator invoiceTranslator,
            IInvoiceService invoiceService,
            IInvoicesBootGridTranslator invoicesBootGridTranslator,
            ICustomerProvider customerProvider,
            IEncryptionService encryptionService)
        {
            this.invoiceTranslator = invoiceTranslator;
            this.invoiceService = invoiceService;
            this.invoicesBootGridTranslator = invoicesBootGridTranslator;
            this.customerProvider = customerProvider;
            this.encryptionService = encryptionService;
        }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public string CreateInvoice(
            IPublishedContent publishedContent,
            CreateInvoiceViewModel viewModel)
        {
            InvoiceModel invoiceModel = invoiceTranslator.Translate(viewModel);

            invoiceService.CreateInvoice(invoiceModel);

            PageModel pageModel = new PageModel(publishedContent);

            return pageModel.NextPageUrl;
        }

        /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        public InvoiceViewModel GetInvoice(
            UmbracoContext umbracoContext,
            string invoiceId)
        {
            string id = encryptionService.DecryptString(invoiceId);

            ClientInvoiceModel model = invoiceService.GetClientInvoice(GetCustomerId(umbracoContext), id);

            if (model != null)
            {
                return invoiceTranslator.Translate(model);
            }

            return null;
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
            IEnumerable<ClientInvoiceModel> models =  invoiceService.GetClientInvoices(GetCustomerId(umbracoContext));

            List<InvoiceViewModel> viewModels = new List<InvoiceViewModel>();

            foreach (ClientInvoiceModel model in models)
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
