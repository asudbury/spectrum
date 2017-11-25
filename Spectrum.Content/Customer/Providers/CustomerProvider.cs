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
        /// Initializes a new instance of the <see cref="CustomerProvider"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public CustomerProvider(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public CustomerModel GetCustomerModel(UmbracoContext umbracoContext)
        {
            IPublishedContent customerNode = settingsService.GetCustomerNode(umbracoContext);

            if (customerNode != null)
            {
                return new CustomerModel(customerNode);
            }

            return null;
        }
    }
}
