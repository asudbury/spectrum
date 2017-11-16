namespace Spectrum.Content.Payments.Managers
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public interface IPaymentManager
    {
        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetAuthToken(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetEnvironment(UmbracoContext umbracoContext);

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns></returns>
        string MakePayment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            MakePaymentViewModel viewModel,
            string currentUser);
    }
}
