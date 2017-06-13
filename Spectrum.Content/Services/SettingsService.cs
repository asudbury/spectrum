namespace Spectrum.Content.Services
{
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// Gets the settings node.
        /// </summary>
        /// <returns></returns>
        public IPublishedContent GetSettingsNode(UmbracoContext context)
        {
            return GetHelper(context).TypedContentAtRoot().FirstOrDefault(x => x.DocumentTypeAlias == "settings");
        }

        /// <summary>
        /// Gets the menus node.
        /// </summary>
        /// <returns></returns>
        public IPublishedContent GetMenusNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);

            return settingsNode?.Children.FirstOrDefault(x => x.DocumentTypeAlias == "menus");
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IPublishedContent GetMenu(
            UmbracoContext context,
            string name)
        {
            IPublishedContent menusNode = GetMenusNode(context);

            return menusNode?.Children.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Gets the helper.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private UmbracoHelper GetHelper(UmbracoContext context)
        {
            return new UmbracoHelper(context);
        }
    }
}
