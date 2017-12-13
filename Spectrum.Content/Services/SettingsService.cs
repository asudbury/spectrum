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
        /// The umbraco context.
        /// </summary>
        private readonly UmbracoContext umbracoContext;

        /// <summary>
        /// The cache.
        /// </summary>
        private static readonly Dictionary<string, IPublishedContent> Cache = new Dictionary<string, IPublishedContent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        public SettingsService()
        {
            umbracoContext = UmbracoContext.Current;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="umbracoContextAccessor">The umbraco context accessor.</param>
        public SettingsService(IUmbracoContextAccessor umbracoContextAccessor)
        {
            umbracoContext = umbracoContextAccessor.Value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            Cache.Clear();
        }

        /// <summary>
        /// Gets the settings node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetSettingsNode()
        {
            if (Cache.ContainsKey(Constants.Nodes.SettingsNodeName))
            {
                return Cache[Constants.Nodes.SettingsNodeName];
            }

            IPublishedContent node = GetHelper().TypedContentAtRoot().FirstOrDefault(x => x.DocumentTypeAlias == "settings") ??
                                     GetHelper().TypedContentAtRoot().FirstOrDefault(x => x.Name == "Settings");

            if (Cache.ContainsKey(Constants.Nodes.SettingsNodeName) == false)
            {
                Cache.Add(Constants.Nodes.SettingsNodeName, node);
            }

            return node;
        }

        /// <summary>
        /// Gets the customer node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetCustomerNode()
        {
            MembershipHelper membershipHelper = new MembershipHelper(umbracoContext);

            IPublishedContent currentMember = membershipHelper.GetCurrentMember();

            if (currentMember != null)
            {
                string currentUserName = currentMember.Name;

                if (Cache.ContainsKey(Constants.Nodes.CustomerNodeName + currentUserName))
                {
                    return Cache[Constants.Nodes.CustomerNodeName + currentUserName];
                }
                
                IPublishedContent settingsNode = GetSettingsNode();

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

        /// <summary>
        /// Gets the menus node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetMenusNode()
        {
            IPublishedContent settingsNode = GetSettingsNode();

            return settingsNode != null ? GetChildNode(
                                            settingsNode, 
                                            "menus", 
                                            Constants.Nodes.MenuNodeName,
                                            null) : null;
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetMenu(string name)
        {
            if (Cache.ContainsKey(Constants.Nodes.MenuNodeName + name))
            {
                return Cache[Constants.Nodes.MenuNodeName + name];
            }

            IPublishedContent menusNode = GetMenusNode();

            IPublishedContent menu =  menusNode?.Children.FirstOrDefault(x => x.Name == name);

            Cache.Add(Constants.Nodes.MenuNodeName + name, menu);
            return menu;
        }

        /// <summary>
        /// Gets the payments node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetPaymentsNode()
        {
            IPublishedContent customerNode = GetCustomerNode();

            return customerNode != null ? GetChildNode(
                                            customerNode, 
                                            "payments", 
                                            Constants.Nodes.PaymentsNodeName,
                                            customerNode) : null;
        }

        /// <summary>
        /// Gets the maile node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetMailNode()
        {
            IPublishedContent customerNode = GetCustomerNode();

            return customerNode != null ? GetChildNode(
                                            customerNode, 
                                            "mail", 
                                            Constants.Nodes.MailNodeName,
                                            customerNode) : null;
        }

        /// <summary>
        /// Gets the mail templates folder node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetMailTemplatesFolderNode()
        {
            IPublishedContent mailNode = GetMailNode();

            return mailNode?.Children.FirstOrDefault();
        }

        /// <summary>
        /// Gets the mail template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetMailTemplate(string templateName)
        {
            IPublishedContent folderNode = GetMailTemplatesFolderNode();

            return folderNode?.Children.FirstOrDefault(x => x.Name == templateName);
        }

        /// <summary>
        /// Gets the mail template.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetMailTemplate(
           string folderName, 
            string templateName)
        {
            IPublishedContent folderNode = GetMailTemplatesFolderNode();

            return folderNode?.Children.FirstOrDefault(x => x.Name == templateName);
        }

        /// <summary>
        /// Gets the mail template by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetMailTemplateById(int id)
        {
            IPublishedContent folderNode = GetMailTemplatesFolderNode();

            return folderNode?.Children.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets the mail template by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetMailTemplateByUrl(string url)
        {
            IPublishedContent mailTemplatesNode = GetMailTemplatesFolderNode();

            return mailTemplatesNode?.Children.FirstOrDefault(x => x.Url == url);
        }

        /// <summary>
        /// Gets the appointments node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetAppointmentsNode()
        {
            IPublishedContent customerNode = GetCustomerNode();

            return customerNode != null ? GetChildNode(
                                            customerNode, 
                                            "appointments", 
                                            Constants.Nodes.AppointmentsNodeName,
                                            customerNode) : null;
        }

        /// <summary>
        /// Gets the quotes node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetQuotesNode()
        {
            IPublishedContent customerNode = GetCustomerNode();

            return customerNode != null ? GetChildNode(
                                                customerNode, 
                                                "quotes", 
                                                Constants.Nodes.QuotesNodeName,
                                                customerNode) : null;
        }

        /// <summary>
        /// Gets the invoices node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetInvoicesNode()
        {
            IPublishedContent customerNode = GetCustomerNode();

            return customerNode != null ? GetChildNode(
                                            customerNode, 
                                            "invoices", 
                                            Constants.Nodes.InvoicesNodeName,
                                            customerNode) : null;
        }

        /// <summary>
        /// Gets the cards node.
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetCardsNode()
        {
            IPublishedContent settingsNode = GetSettingsNode();

            return settingsNode != null ? GetChildNode(settingsNode, "cards", Constants.Nodes.CardsNodeName, null) : null;
        }

        /// <summary>
        /// Gets the card stack.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public IPublishedContent GetCardStack(string name)
        {
            IPublishedContent cardsNode = GetCardsNode();

            GetChildNode(cardsNode, name, Constants.Nodes.CardsNodeName + name, null);

            return cardsNode?.Children.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Gets the helper.
        /// </summary>
        /// <returns></returns>
        private UmbracoHelper GetHelper()
        {
            return new UmbracoHelper(umbracoContext);
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
