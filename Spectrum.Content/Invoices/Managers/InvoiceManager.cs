namespace Spectrum.Content.Invoices.Managers
{
    using Content.Models;
    using Content.Services;
    using ContentModels;
    using Customer.Managers;
    using Customer.Providers;
    using Customer.ViewModels;
    using Mail.Providers;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Scorchio.Services;
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
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

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
        /// The mail provider.
        /// </summary>
        private readonly IMailProvider mailProvider;

        /// <summary>
        /// The URL service.
        /// </summary>
        private readonly IUrlService urlService;

        /// <summary>
        /// The client manager.
        /// </summary>
        private readonly IClientManager clientManager;

        /// <summary>
        /// The token service.
        /// </summary>
        private readonly ITokenService tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceManager" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="invoiceTranslator">The invoice translator.</param>
        /// <param name="invoiceService">The invoice service.</param>
        /// <param name="invoicesBootGridTranslator">The invoices boot grid translator.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="mailProvider">The mail provider.</param>
        /// <param name="urlService">The URL service.</param>
        /// <param name="clientManager">The client manager.</param>
        /// <param name="tokenService">The token service.</param>
        public InvoiceManager(
            ISettingsService settingsService,
            IInvoiceTranslator invoiceTranslator,
            IInvoiceService invoiceService,
            IInvoicesBootGridTranslator invoicesBootGridTranslator,
            ICustomerProvider customerProvider,
            IEncryptionService encryptionService,
            IMailProvider mailProvider,
            IUrlService urlService,
            IClientManager clientManager,
            ITokenService tokenService)
        {
            this.settingsService = settingsService;
            this.invoiceTranslator = invoiceTranslator;
            this.invoiceService = invoiceService;
            this.invoicesBootGridTranslator = invoicesBootGridTranslator;
            this.customerProvider = customerProvider;
            this.encryptionService = encryptionService;
            this.mailProvider = mailProvider;
            this.urlService = urlService;
            this.clientManager = clientManager;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public string CreateInvoice(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            CreateInvoiceViewModel viewModel)
        {
            InvoiceModel invoiceModel = invoiceTranslator.Translate(viewModel);

            int invoiceId = invoiceService.CreateInvoice(invoiceModel);

            PageModel pageModel = new PageModel(publishedContent);

            if (viewModel.EmailInvoiceToClient)
            {
                ClientViewModel clientViewModel = clientManager.GetClient(viewModel.Code);

                IPublishedContent customerNode = settingsService.GetCustomerNode();

                CustomerModel customerModel = new CustomerModel(customerNode);

                string paymentLinkUrl = urlService.GetCustomerMakePaymentUrl(
                                            customerModel.Id,
                                            clientViewModel.Id,
                                            invoiceId,
                                            viewModel.Amount.ToString());

                Dictionary<string, string> dictionary = tokenService.GetBaseTokens(customerModel, clientViewModel.Name);

               dictionary.Add("InvoiceId", invoiceId.ToString());
               dictionary.Add("InvoiceDate", viewModel.Date.ToLongTimeString());
               dictionary.Add("InvoiceAmount", viewModel.Amount.ToString());
               dictionary.Add("InvoiceDetails", viewModel.Details);
               dictionary.Add("PaymentLinkUrl", paymentLinkUrl);

                //// now send email
                mailProvider.SendEmail(
                    umbracoContext,
                    pageModel.EmailTemplateName,
                    customerModel.EmailAddress,
                    clientViewModel.EmailAddress,
                    string.Empty,
                    null,
                    dictionary);
            }

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
            IEnumerable<ClientInvoiceModel> models = invoiceService.GetClientInvoices(GetCustomerId(umbracoContext));

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
