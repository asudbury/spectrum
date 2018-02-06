namespace Spectrum.Content.Customer.Providers
{
    using Content.Services;
    using ContentModels;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class CustomerProvider : ICustomerProvider
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProvider" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public CustomerProvider(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public CustomerModel GetCustomerModel(int? customerId = null)
        {
            return GetCustomerModel(
                UmbracoContext.Current, 
                customerId);
        }

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public CustomerModel GetCustomerModel(
            UmbracoContext umbracoContext,
            int? customerId = null)
        {
            IPublishedContent customerNode = settingsService.GetCustomerNode(customerId);

            if (customerNode != null)
            {
                return new CustomerModel(customerNode);
            }

            return null;
        }
    }
}
