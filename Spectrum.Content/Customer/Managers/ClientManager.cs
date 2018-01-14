using Spectrum.Content.Components.ViewModels;

namespace Spectrum.Content.Customer.Managers
{
    using Application.Services;
    using Content.Models;
    using Content.Services;
    using ContentModels;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Providers;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Translators;
    using Umbraco.Web;
    using ViewModels;

    public class ClientManager : IClientManager
    {
        /// <summary>
        /// The client service.
        /// </summary>
        private readonly IClientService clientService;

        /// <summary>
        /// The client translator.
        /// </summary>
        private readonly IClientTranslator clientTranslator;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// The clients boot grid translator
        /// </summary>
        private readonly IClientsBootGridTranslator clientsBootGridTranslator;

        /// <summary>
        /// The URL service.
        /// </summary>
        private readonly IUrlService urlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientManager" /> class.
        /// </summary>
        /// <param name="clientService">The client service.</param>
        /// <param name="clientTranslator">The client translator.</param>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="clientsBootGridTranslator">The clients boot grid translator.</param>
        /// <param name="urlService">The URL service.</param>
        public ClientManager(
            IClientService clientService,
            IClientTranslator clientTranslator,
            IEncryptionService encryptionService,
            ICustomerProvider customerProvider,
            IClientsBootGridTranslator clientsBootGridTranslator,
            IUrlService urlService)
        {
            this.clientService = clientService;
            this.clientTranslator = clientTranslator;
            this.encryptionService = encryptionService;
            this.customerProvider = customerProvider;
            this.clientsBootGridTranslator = clientsBootGridTranslator;
            this.urlService = urlService;
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public string CreateClient(CreateClientViewModel viewModel)
        {
            ClientModel model = clientTranslator.Translate(viewModel);

            int clientId = clientService.CreateClient(model);

            string url = urlService.GetViewClientUrl(clientId);

            return url;
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="encryptedId">The encrypted identifier.</param>
        /// <returns></returns>
        public ClientViewModel GetClient(string encryptedId)
        {
            string clientId = encryptionService.DecryptString(encryptedId);

            CustomerModel customerModel = customerProvider.GetCustomerModel();
            
            ClientModel model = clientService.GetClient(customerModel.Id, Convert.ToInt32(clientId));

            if (model != null)
            {
                return clientTranslator.Translate(model);
            }
            
            return null;
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientViewModel> GetClients()
        {
            CustomerModel customerModel = customerProvider.GetCustomerModel();

            IEnumerable<ClientModel> models = clientService.GetClients(customerModel.Id);

            List<ClientViewModel> viewModels = new List<ClientViewModel>();

            foreach (ClientModel clientModel in models)
            {
                viewModels.Add(clientTranslator.Translate(clientModel));
            }

            return viewModels;
        }

        /// <summary>
        /// Gets the boot grid clients.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public string GetBootGridClients(
            int current,
            int rowCount,
            string searchPhrase, IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext)
        {
            IEnumerable<ClientViewModel> viewModels = GetClients();

            BootGridViewModel<ClientViewModel> bootGridViewModel = clientsBootGridTranslator.Translate(
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
        /// Checks the name in use.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool CheckNameInUse(string name)
        {
            CustomerModel customerModel = customerProvider.GetCustomerModel();

            IEnumerable<ClientModel> models = clientService.GetClients(customerModel.Id);

            foreach (ClientModel clientModel in models)
            {
               ClientViewModel viewModel = clientTranslator.Translate(clientModel);

                if (viewModel.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks the email in use.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        public bool CheckEmailInUse(string emailAddress)
        {
            CustomerModel customerModel = customerProvider.GetCustomerModel();

            IEnumerable<ClientModel> models = clientService.GetClients(customerModel.Id);

            foreach (ClientModel clientModel in models)
            {
                ClientViewModel viewModel = clientTranslator.Translate(clientModel);

                if (viewModel.EmailAddress == emailAddress)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the name of the client.
        /// </summary>
        /// <returns></returns>
        public LinkViewModel GetClientName(string clientId)
        {
            ClientViewModel viewModel = GetClient(clientId);

            if (viewModel != null)
            {
                LinkViewModel linkViewModel = new LinkViewModel
                {
                    Text = viewModel.Name,
                    Url = urlService.GetViewClientUrl(viewModel.Id)
                };

                return linkViewModel;
            }

            return null;
        }
    }
}
