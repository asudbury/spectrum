namespace Spectrum.Content.Services
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public interface ISettingsService
    {
        /// <summary>
        /// Gets the settings node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IPublishedContent GetSettingsNode(UmbracoContext context);

        /// <summary>
        /// Gets the menus node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IPublishedContent GetMenusNode(UmbracoContext context);

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IPublishedContent GetMenu(
            UmbracoContext context,
            string name);

    }
}
