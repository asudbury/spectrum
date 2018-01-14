namespace Spectrum.Content.Customer.Providers
{
    using ContentModels;
    using Umbraco.Web;

    public interface ICustomerProvider
    {
        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <returns></returns>
        CustomerModel GetCustomerModel();

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        CustomerModel GetCustomerModel(UmbracoContext umbracoContext);
    }
}