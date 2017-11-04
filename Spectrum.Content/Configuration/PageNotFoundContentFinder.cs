namespace Spectrum.Content.Configuration
{
    using ContentModels;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Routing;

    public class PageNotFoundContentFinder : IContentFinder
    {
        /// <inheritdoc />
        /// <summary>
        /// Tries to find and assign an Umbraco document to a <c>PublishedContentRequest</c>.
        /// </summary>
        /// <param name="contentRequest">The <c>PublishedContentRequest</c>.</param>
        /// <returns>A value indicating whether an Umbraco document was found and assigned.</returns>
        /// <remarks>Optionally, can also assign the template or anything else on the document request, although that is not required.</remarks>
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            LogHelper.Info(GetType(), "404 Finder triggered");

            bool isBackOfficeUserLoggedin = ConfigurationHelper.IsBackOfficeUserLoggedIn();

            if (isBackOfficeUserLoggedin)
            {
                return true;
            }

            //// lets looks for the virtual customer homepage!

            IPublishedContent settingsNode =  ConfigurationHelper.GetSettingsNode();

            if (settingsNode != null)
            {
                IEnumerable<IPublishedContent> customerNodes = settingsNode.Children.Where(x => x.DocumentTypeAlias == "customer");

                foreach (IPublishedContent customerNode in customerNodes)
                {
                    IPublishedContent homePageContent = GetCustomerHomePage(contentRequest.Uri.AbsolutePath, customerNode);

                    if (homePageContent != null)
                    {
                        contentRequest.PublishedContent = homePageContent;
                        return true;
                    }
                }
            }

            IPublishedContent node = FindContent(contentRequest, contentRequest.Uri.GetAbsolutePathDecoded());

            if (node != null)
            {
                return true;
            }

            //// now handle 404 pages!
            SettingsModel settingsModel = new SettingsModel(settingsNode);

            if (string.IsNullOrEmpty(settingsModel.PageNotFoundUrl) == false)
            {
                contentRequest.PublishedContent = new UmbracoHelper(UmbracoContext.Current).TypedContent(settingsModel.PageNotFoundNodeId);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Finds the content.
        /// </summary>
        /// <param name="contentRequest">The content request.</param>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        protected IPublishedContent FindContent(
            PublishedContentRequest contentRequest,
            string route)
        {
            IPublishedContent node = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetByRoute(route);

            if (node != null)
            {
                contentRequest.PublishedContent = node;
            }

            return node;
        }

        /// <summary>
        /// Gets the customer home page.
        /// </summary>
        /// <param name="uriAbsolutePath">The URI absolute path.</param>
        /// <param name="customerNode">The customer node.</param>
        /// <returns></returns>
        internal IPublishedContent GetCustomerHomePage(
            string uriAbsolutePath,
            IPublishedContent customerNode)
        {
            CustomerModel customerModel = new CustomerModel(customerNode);

            string homePageUrl = customerModel.HomePageUrl;

            if (string.IsNullOrEmpty(homePageUrl) == false)
            {
                if (uriAbsolutePath.Contains(homePageUrl))
                {
                    IPublishedContent homePage = customerNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "home");

                    if (homePage != null)
                    {
                        return homePage;
                    }
                }
            }

            return null;
        }
    }
}
