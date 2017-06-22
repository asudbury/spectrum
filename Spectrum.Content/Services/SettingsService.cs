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
        /// Gets the payments node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetPaymentsNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);

            return settingsNode?.Children.FirstOrDefault(x => x.DocumentTypeAlias == "payments");
        }
        
        /// <summary>
        /// Gets the maile node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetMailNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);

            return settingsNode?.Children.FirstOrDefault(x => x.DocumentTypeAlias == "mail");
        }
        
        /// <summary>
        /// Gets the mail templates folder node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetMailTemplatesFolderNode(UmbracoContext context)
        {
            IPublishedContent templatesNode = GetMailNode(context);

            return templatesNode?.Children.FirstOrDefault(x => x.Name == "mailTemplates");
        }

        /// <summary>
        /// Gets the mail template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        public IPublishedContent GetMailTemplate(
            UmbracoContext context, 
            string folderName, 
            string templateName)
        {
            IPublishedContent folderNode = GetMailTemplatesFolderNode(context);

            return folderNode?.Children.FirstOrDefault(x => x.Name == templateName);
        }

        /// <summary>
        /// Gets the mail template by identifier.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IPublishedContent GetMailTemplateById(
            UmbracoContext context, 
            int id)
        {
            IPublishedContent folderNode = GetMailTemplatesFolderNode(context);

            return folderNode?.Children.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets the appointments node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetAppointmentsNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);

            return settingsNode?.Children.FirstOrDefault(x => x.DocumentTypeAlias == "appointments");
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
