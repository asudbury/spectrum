namespace Spectrum.Content.Services
{
    using ContentModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;

    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private static readonly Dictionary<string, IPublishedContent> Cache = new Dictionary<string, IPublishedContent>();
        
        /// <inheritdoc />
        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            Cache.Clear();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the settings node.
        /// </summary>
        /// <returns></returns>
        public IPublishedContent GetSettingsNode(UmbracoContext context)
        {
            if (Cache.ContainsKey(Constants.Nodes.SettingsNodeName))
            {
                return Cache[Constants.Nodes.SettingsNodeName];
            }

            IPublishedContent node = GetHelper(context).TypedContentAtRoot().FirstOrDefault(x => x.DocumentTypeAlias == "settings") ??
                                     GetHelper(context).TypedContentAtRoot().FirstOrDefault(x => x.Name == "Settings");

            Cache.Add(Constants.Nodes.SettingsNodeName, node);

            return node;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the customer node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetCustomerNode(UmbracoContext context)
        {
            MembershipHelper membershipHelper = new MembershipHelper(context);

            IPublishedContent currentMember = membershipHelper.GetCurrentMember();

            if (currentMember != null)
            {
                string currentUserName = currentMember.Name;

                if (Cache.ContainsKey(Constants.Nodes.CustomerNodeName + currentUserName))
                {
                    return Cache[Constants.Nodes.CustomerNodeName + currentUserName];
                }
                
                IPublishedContent settingsNode = GetSettingsNode(context);

                if (settingsNode != null)
                {
                    IEnumerable<IPublishedContent> customerNodes = settingsNode.Children.Where(x => x.DocumentTypeAlias == "customer");

                    foreach (IPublishedContent customerNode in customerNodes)
                    {
                        CustomerModel customerModel = new CustomerModel(customerNode);

                        if (customerModel.Users.Contains(currentUserName))
                        {
                            if (customerModel.CacheSettings)
                            {
                                Cache.Add(Constants.Nodes.CustomerNodeName + currentUserName, customerNode);
                            }

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

            return settingsNode != null ? GetChildNode(
                                            settingsNode, 
                                            "menus", 
                                            Constants.Nodes.MenuNodeName,
                                            null) : null;
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
            if (Cache.ContainsKey(Constants.Nodes.MenuNodeName + name))
            {
                return Cache[Constants.Nodes.MenuNodeName + name];
            }

            IPublishedContent menusNode = GetMenusNode(context);

            IPublishedContent menu =  menusNode?.Children.FirstOrDefault(x => x.Name == name);

            Cache.Add(Constants.Nodes.MenuNodeName + name, menu);
            return menu;
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

            return customerNode != null ? GetChildNode(
                                            customerNode, 
                                            "payments", 
                                            Constants.Nodes.PaymentsNodeName,
                                            customerNode) : null;
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

            return customerNode != null ? GetChildNode(
                                            customerNode, 
                                            "mail", 
                                            Constants.Nodes.MailNodeName,
                                            customerNode) : null;
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

            return customerNode != null ? GetChildNode(
                                            customerNode, 
                                            "appointments", 
                                            Constants.Nodes.AppointmentsNodeName,
                                            customerNode) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the quotes node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetQuotesNode(UmbracoContext context)
        {
            IPublishedContent customerNode = GetCustomerNode(context);

            return customerNode != null ? GetChildNode(
                                                customerNode, 
                                                "quotes", 
                                                Constants.Nodes.QuotesNodeName,
                                                customerNode) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the invoices node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetInvoicesNode(UmbracoContext context)
        {
            IPublishedContent customerNode = GetCustomerNode(context);

            return customerNode != null ? GetChildNode(
                                            customerNode, 
                                            "invoices", 
                                            Constants.Nodes.InvoicesNodeName,
                                            customerNode) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the cards node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IPublishedContent GetCardsNode(UmbracoContext context)
        {
            IPublishedContent settingsNode = GetSettingsNode(context);

            return settingsNode != null ? GetChildNode(settingsNode, "cards", Constants.Nodes.CardsNodeName, null) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the card stack.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IPublishedContent GetCardStack(
            UmbracoContext context, 
            string name)
        {
            IPublishedContent cardsNode = GetCardsNode(context);

            GetChildNode(cardsNode, name, Constants.Nodes.CardsNodeName + name, null);

            return cardsNode?.Children.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
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

        /// <summary>
        /// Gets the child node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="childNodeName">Name of the child node.</param>
        /// <param name="cacheKeyName">Name of the cache key.</param>
        /// <param name="customerNode">The customer node.</param>
        /// <returns></returns>
        private IPublishedContent GetChildNode(
            IPublishedContent node,
            string childNodeName,
            string cacheKeyName,
            IPublishedContent customerNode)
        {
            bool useCache = true;

            if (customerNode != null)
            {
                CustomerModel customerModel = new CustomerModel(customerNode);

                useCache = customerModel.CacheSettings;
            }

            if (useCache)
            {
                if (string.IsNullOrEmpty(cacheKeyName) == false)
                {
                    if (Cache.ContainsKey(cacheKeyName))
                    {
                        return Cache[cacheKeyName];
                    }
                }
            }

            IPublishedContent childNode = node.Children.FirstOrDefault(x => x.DocumentTypeAlias.ToLower() == childNodeName.ToLower()) ??
                                          node.Children.FirstOrDefault(x => x.Name.ToLower() == childNodeName.ToLower());

            if (useCache)
            {
                if (string.IsNullOrEmpty(cacheKeyName) == false)
                {
                    Cache.Add(cacheKeyName, childNode);
                }
            }

            return childNode;
        }
    }
}
