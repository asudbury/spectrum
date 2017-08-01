namespace Spectrum.Content.Correspondence.Manangers
{
    using ViewModels;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public interface IContactUsManager
    {
        /// <summary>
        /// Inserts the contact us.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        string InsertContactUs(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            ContactUsViewModel viewModel);
    }
}
