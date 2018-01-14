namespace Spectrum.Content.Customer.Translators
{
    using Application.Services;
    using Content.Services;
    using ContentModels;
    using Models;
    using System;
    using Umbraco.Core.Models;
    using ViewModels;

    public class ClientTranslator : IClientTranslator
    {
        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The URL service.
        /// </summary>
        private readonly IUrlService urlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTranslator" /> class.
        /// </summary>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="urlService">The URL service.</param>
        public ClientTranslator(
            IEncryptionService encryptionService,
             IUserService userService,
             ISettingsService settingsService,
             IUrlService urlService)
        {
            this.encryptionService = encryptionService;
            this.userService = userService;
            this.settingsService = settingsService;
            this.urlService = urlService;
        }

        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public ClientModel Translate(CreateClientViewModel viewModel)
        {
            IPublishedContent customerNode = settingsService.GetCustomerNode();

            CustomerModel customerModel = new CustomerModel(customerNode);

            ClientModel model = new ClientModel
            {
                CreatedTime = DateTime.Now,
                CreatedUser = userService.GetCurrentUserName(),
                LastUpdatedTime = DateTime.Now,
                LastUpdatedUser = userService.GetCurrentUserName(),
                CustomerId = customerModel.Id,
                Name = encryptionService.EncryptString(viewModel.Name),
                EmailAddress = encryptionService.EncryptString(viewModel.EmailAddress),
                HomePhoneNumber = encryptionService.EncryptString(viewModel.HomePhoneNumber),
                MobilePhoneNumber = encryptionService.EncryptString(viewModel.MobilePhoneNumber),
                BuildingNumber = encryptionService.EncryptString(viewModel.BuildingNumber),
                PostCode = encryptionService.EncryptString(viewModel.PostCode),
                Address = encryptionService.EncryptString(viewModel.Address),
                Notes = string.Empty
            };
            
            return model;
        }

        /// <summary>
        /// Translates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ClientViewModel Translate(ClientModel model)
        {
            ClientViewModel viewModel = new ClientViewModel
            {
                Id = model.Id,
                CreatedTime = model.CreatedTime,
                CreatedUser = model.CreatedUser,
                LastUpdatedTime = model.LastUpdatedTime,
                LastUpdatedUser = model.LastUpdatedUser,
                Name = encryptionService.DecryptString(model.Name),
                GoogleSearchUrl = GetGoogleSearchUrl(model),
                EmailAddress = encryptionService.DecryptString(model.EmailAddress),
                HomePhoneNumber = encryptionService.DecryptString(model.HomePhoneNumber),
                MobilePhoneNumber = encryptionService.DecryptString(model.MobilePhoneNumber),
                Notes = encryptionService.DecryptString(model.Notes),
                BuildingNumber = encryptionService.DecryptString(model.BuildingNumber),
                PostCode = encryptionService.DecryptString(model.PostCode),
                Address = encryptionService.DecryptString(model.Address),
                CreateQuoteUrl = urlService.GetCreateQuoteUrl(model.Id),
                CreateInvoiceUrl = urlService.GetCreateInvoiceUrl(model.Id),
                CreateAppointmentUrl = urlService.GetCreateAppointmentUrl(model.Id),
                ViewClientUrl = urlService.GetViewClientUrl(model.Id),
                UpdateClientUrl = urlService.GetUpdateClientUrl(model.Id)
            };

            return viewModel;
        }

        /// <summary>
        /// Gets the google search URL.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        internal string GetGoogleSearchUrl(ClientModel model)
        {
            string searchString;

            if (string.IsNullOrEmpty(model.Address) == false)
            {
                searchString = encryptionService.DecryptString(model.Address);
            }

            else
            {
                searchString = encryptionService.DecryptString(model.PostCode) + " ";
                encryptionService.DecryptString(model.BuildingNumber);
            }

            return urlService.GetGoogleSearchUrl(searchString);
        }
    }
}
