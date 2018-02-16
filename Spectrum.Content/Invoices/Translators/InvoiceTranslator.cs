namespace Spectrum.Content.Invoices.Translators
{
    using Application.Services;
    using Content.Services;
    using ContentModels;
    using Models;
    using System;
    using System.Globalization;
    using Umbraco.Core.Models;
    using ViewModels;

    public class InvoiceTranslator : IInvoiceTranslator
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// The URL service.
        /// </summary>
        private readonly IUrlService urlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceTranslator" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="urlService">The URL service.</param>
        public InvoiceTranslator(
            ISettingsService settingsService,
            IUserService userService,
            IEncryptionService encryptionService,
            IUrlService urlService)
        {
            this.settingsService = settingsService;
            this.userService = userService;
            this.encryptionService = encryptionService;
            this.urlService = urlService;
        }

        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public InvoiceModel Translate(CreateInvoiceViewModel viewModel)
        {
            IPublishedContent publishedContent = settingsService.GetCustomerNode();

            CustomerModel model = new CustomerModel(publishedContent);

            return new InvoiceModel
            {
                ClientId = Convert.ToInt32(encryptionService.DecryptString(viewModel.Code)),
                CustomerId = model.Id,
                CreatedTime = DateTime.Now,
                CreatedUser = userService.GetCurrentUserName(),
                LastUpdatedTime = DateTime.Now,
                LastUpdatedUser = userService.GetCurrentUserName(),
                InvoiceDate = viewModel.Date,
                InvoiceAmount = Convert.ToDecimal(viewModel.Amount),
                InvoiceDetails =  viewModel.Details
            };
        }

        /// <summary>
        /// Translates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public InvoiceViewModel Translate(ClientInvoiceModel model)
        {
            decimal amount = Math.Round(model.InvoiceAmount, 2);
            
            return new InvoiceViewModel
            {
                Id = model.Id,
                InvoiceDate = model.InvoiceDate,
                Amount = "£" + Math.Round(model.InvoiceAmount, 2),
                Details = model.InvoiceDetails,
                PaymentId = model.PaymentId,
                ViewInvoiceUrl = urlService.GetViewInvoiceUrl(model.ClientId, model.Id),
                UpdateInvoiceUrl = urlService.GetUpdateInvoiceUrl(model.ClientId, model.Id),
                EmailInvoiceUrl = urlService.GetEmailInvoiceUrl(model.ClientId, model.Id),
                MakePaymentUrl = urlService.GetMakePaymentUrl(model.ClientId, model.Id, amount.ToString(CultureInfo.InvariantCulture)),
                ViewPaymentUrl = urlService.GetViewPaymentUrl(model.ClientId, model.PaymentId),
                ClientName = encryptionService.DecryptString(model.ClientName),
                ClientUrl = urlService.GetViewClientUrl(model.ClientId),
                CreatedTime = model.CreatedTime,
                CreatedUser = model.CreatedUser,
                LastUpdatedTime = model.LastUpdatedTime,
                LastedUpdatedUser = model.LastUpdatedUser
            };
        }
    }
}
