using Spectrum.Content.ContentModels;

namespace Spectrum.Content.Customer.Managers
{
    using Umbraco.Web;

    public interface ICustomerManager
    {
        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="encryptedCustomerId">The encrypted customer identifier.</param>
        /// <returns></returns>
        CustomerModel GetCustomerModel(string encryptedCustomerId = null);

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="encryptedCustomerId">The encrypted customer identifier.</param>
        /// <returns></returns>
        CustomerModel GetCustomerModel(
            UmbracoContext umbracoContext,
            string encryptedCustomerId = null);
    }
}