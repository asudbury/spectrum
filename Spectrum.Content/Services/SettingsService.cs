namespace Spectrum.Content.Services
{
    using ContentModels;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;

    public class SettingsService : ISettingsService
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets the settings node.
        /// </summary>
        /// <returns></returns>
        public IPublishedContent GetSettingsNode(UmbracoContext context)
        {
            IPublishedContent node = GetHelper(context).TypedContentAtRoot().FirstOrDefault(x => x.DocumentTypeAlias == "settings");

            return node ?? GetHelper(context).TypedContentAtRoot().FirstOrDefault(x => x.Name == "Settings");
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the customer node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetCustomerNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);
        
            if (settingsNode != null)
            {
                MembershipHelper membershipHelper = new MembershipHelper(context);

                IPublishedContent currentMember = membershipHelper.GetCurrentMember();

                if (currentMember != null)
                {
                    string currentUserName = currentMember.Name;

                    IEnumerable<IPublishedContent> customerNodes = settingsNode.Children.Where(x => x.DocumentTypeAlias == "customer");

                    foreach (IPublishedContent customerNode in customerNodes)
                    {
                        CustomerModel customerModel = new CustomerModel(customerNode);

                        if (customerModel.Users.Contains(currentUserName))
                        {
                            return customerNode;
                        }
                    }
                }
            }

            return null;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        /// <summary>
        /// Gets the payments node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetPaymentsNode(UmbracoContext context)
        {
            IPublishedContent customerNode = GetCustomerNode(context);

            if (customerNode != null)
            {
                IPublishedContent node = customerNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "payments");

                return node ?? customerNode.Children.FirstOrDefault(x => x.Name == "Payments");
            }

            return null;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Gets the maile node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetMailNode(UmbracoContext context)
        {
            IPublishedContent customerNode = GetCustomerNode(context);

            if (customerNode != null)
            {
                IPublishedContent node = customerNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "mail");

                return node ?? customerNode.Children.FirstOrDefault(x => x.Name == "Mail");
            }

            return null;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Gets the mail templates folder node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetMailTemplatesFolderNode(UmbracoContext context)
        {
            IPublishedContent mailNode = GetMailNode(context);

            return mailNode?.Children.FirstOrDefault();
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        /// <summary>
        /// Gets the appointments node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetAppointmentsNode(UmbracoContext context)
        {
            IPublishedContent customerNode = GetCustomerNode(context);

            if (customerNode != null)
            {
                IPublishedContent node = customerNode.Children.FirstOrDefault(x => x.DocumentTypeAlias == "appointments");

                return node ?? customerNode.Children.FirstOrDefault(x => x.Name == "Appointments");
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
