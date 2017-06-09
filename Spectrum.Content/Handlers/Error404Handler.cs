namespace Spectrum.Content.Handlers
{
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web.Routing;

    public class Error404Handler : IContentFinder
    {
        /// <summary>
        /// Tries to find and assign an Umbraco document to a <c>PublishedContentRequest</c>.
        /// </summary>
        /// <param name="contentRequest">The <c>PublishedContentRequest</c>.</param>
        /// <returns>A value indicating whether an Umbraco document was found and assigned.</returns>
        /// <remarks>Optionally, can also assign the template or anything else on the document request, although that is not required.</remarks>
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            if (contentRequest.PublishedContent == null)
            {
                IPublishedContent home = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetAtRoot().First();

                IPublishedContent notFoundNode = home.Children.Single(x => x.Name == "Error-404");

                contentRequest.SetResponseStatus(404, "404 Page Not Found");
                
                contentRequest.PublishedContent = notFoundNode;
            }

            return contentRequest.PublishedContent != null;
        }
    }
}
