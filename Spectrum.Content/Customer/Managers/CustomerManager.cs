namespace Spectrum.Content.Customer.Managers
{
    using ContentModels;
    using Providers;
    using Scorchio.Services;
    using Umbraco.Web;

    public class CustomerManager : ICustomerManager
    {
        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// The customer provider
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerManager"/> class.
        /// </summary>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="customerProvider">The customer provider.</param>
        public CustomerManager(
            IEncryptionService encryptionService,
            ICustomerProvider customerProvider)
        {
            this.encryptionService = encryptionService;
            this.customerProvider = customerProvider;
        }

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="encryptedCustomerId">The encrypted customer identifier.</param>
        /// <returns></returns>
        public CustomerModel GetCustomerModel(string encryptedCustomerId = null)
        {
            int? customerId = encryptionService.DecryptNumber(encryptedCustomerId);

           return  customerProvider.GetCustomerModel(customerId);
        }

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="encryptedCustomerId">The encrypted customer identifier.</param>
        /// <returns></returns>
        public CustomerModel GetCustomerModel(
            UmbracoContext umbracoContext, 
            string encryptedCustomerId = null)
        {
            int? customerId = encryptionService.DecryptNumber(encryptedCustomerId);

            return customerProvider.GetCustomerModel(umbracoContext, customerId);
        }
    }
}
