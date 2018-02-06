namespace Spectrum.Content.Customer.Providers
{
    using ContentModels;
    using Umbraco.Web;

    public interface ICustomerProvider
    {
        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        CustomerModel GetCustomerModel(int? customerId = null);

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        CustomerModel GetCustomerModel(
            UmbracoContext umbracoContext,
            int? customerId = null);
    }
}