namespace Spectrum.Content.Repositories
{
    using ContentModels;
    using Application.Services;
    using Services;
    using System;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class TransactionsRepository : BaseRepository, ITransactionsRepository
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Repositories.TransactionsRepository" /> class.
        /// </summary>
        /// <param name="cacheService">The cache service.</param>
        /// <param name="settingsService">The settings service.</param>
        public TransactionsRepository(
            ICacheService cacheService,
            ISettingsService settingsService)
            :base(cacheService)
        {
            this.settingsService = settingsService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the key.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        public void SetKey(UmbracoContext umbracoContext)
        {
            IPublishedContent customerNode = settingsService.GetCustomerNode();

            if (customerNode != null)
            { 
                CustomerModel customerModel = new CustomerModel(customerNode);

                IPublishedContent paymentsNode = settingsService.GetPaymentsNode();

                if (paymentsNode != null)
                {
                    PaymentSettingsModel paymentSettingsModel = new PaymentSettingsModel(paymentsNode);

                    Key = "Tranactions" + 
                            customerModel.Id + 
                            paymentSettingsModel.Environment +
                            paymentSettingsModel.MerchantId + 
                            paymentSettingsModel.PublicKey;
                    return;

                }
            }

            throw new ApplicationException("Unable to set Transactions Cache Key.");
        }
    }
}
