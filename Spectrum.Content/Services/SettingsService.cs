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
            IPublishedContent node = GetHelper(context).TypedContentAtRoot().FirstOrDefault(x => x.DocumentTypeAlias == "settings");

            return node ?? GetHelper(context).TypedContentAtRoot().FirstOrDefault(x => x.Name == "Settings");
        }

        /// <summary>
        /// Gets the menus node.
        /// </summary>
        /// <returns></returns>
        public IPublishedContent GetMenusNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);

            if (settingsNode != null)
            {
                IPublishedContent node = settingsNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "menus");

                return node ?? settingsNode.Children.FirstOrDefault(x => x.Name == "Menus");
            }

            return null;
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

            if (settingsNode != null)
            {
                IPublishedContent node = settingsNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "payments");

                return node ?? settingsNode.Children.FirstOrDefault(x => x.Name == "Payments");
            }

            return null;
        }
        
        /// <summary>
        /// Gets the maile node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetMailNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);

            if (settingsNode != null)
            {
                IPublishedContent node = settingsNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "mail");

                return node ?? settingsNode.Children.FirstOrDefault(x => x.Name == "Mail");
            }

            return null;
        }
        
        /// <summary>
        /// Gets the mail templates folder node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetMailTemplatesFolderNode(UmbracoContext context)
        {
            IPublishedContent mailNode = GetMailNode(context);

            if (mailNode != null)
            {
                IPublishedContent node = mailNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "mailTemplates");

                return node ?? mailNode.Children.FirstOrDefault(x => x.Name == "Mail Templates");
            }

            return null;

        }

        /// <summary>
        /// Gets the mail template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        public IPublishedContent GetMailTemplate(
            UmbracoContext context, 
            string templateName)
        {
            IPublishedContent folderNode = GetMailTemplatesFolderNode(context);

            return folderNode?.Children.FirstOrDefault(x => x.Name == templateName);
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
        /// Gets the mail template by URL.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public IPublishedContent GetMailTemplateByUrl(
            UmbracoContext context, 
            string url)
        {
            IPublishedContent mailTemplatesNode = GetMailTemplatesFolderNode(context);

            return mailTemplatesNode?.Children.FirstOrDefault(x => x.Url == url);
        }

        /// <summary>
        /// Gets the appointments node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetAppointmentsNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);

            if (settingsNode != null)
            {
                IPublishedContent node = settingsNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "appointments");

                return node ?? settingsNode.Children.FirstOrDefault(x => x.Name == "Appointments");
            }

            return null;
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
