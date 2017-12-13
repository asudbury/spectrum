namespace Spectrum.Content.Invoices.Translators
{
    using Content.Services;
    using ContentModels;
    using Models;
    using System;
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
        /// Initializes a new instance of the <see cref="InvoiceTranslator" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="userService">The user service.</param>
        public InvoiceTranslator(
            ISettingsService settingsService,
            IUserService userService)
        {
            this.settingsService = settingsService;
            this.userService = userService;
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
                CustomerId = model.Id,
                CreatedTime = DateTime.Now,
                CreatedUser = userService.GetCurrentUserName(),
                LasteUpdatedTime = DateTime.Now,
                LastedUpdatedUser = userService.GetCurrentUserName(),
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
        public InvoiceViewModel Translate(InvoiceModel model)
        {
            return new InvoiceViewModel
            {
                Id = model.Id,
                InvoiceDate = model.InvoiceDate,
                Amount = "£" + Math.Round(model.InvoiceAmount, 2),
                ClientName = model.ClientId.ToString(),
                ViewInvoiceUrl = "ViewInvoice"
            };
        }
    }
}
