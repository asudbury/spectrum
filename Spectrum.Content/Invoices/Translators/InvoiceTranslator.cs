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
        /// Initializes a new instance of the <see cref="InvoiceTranslator" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public InvoiceTranslator(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
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
                LasteUpdatedTime = DateTime.Now,
                InvoiceDate = viewModel.Date,
                InvoiceAmount = Convert.ToDecimal(viewModel.Amount),
                InvoiceDetails =  viewModel.Details
            };
        }
    }
}
