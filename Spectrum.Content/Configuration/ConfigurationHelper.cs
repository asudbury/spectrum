namespace Spectrum.Content.Configuration
{
    using ContentModels;
    using Services;
    using System.Web;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Core.Security;
    using Umbraco.Web.Routing;

    public static class ConfigurationHelper
    {
        /// <summary>
        /// Determines whether [is back office user logged in].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is back office user logged in]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBackOfficeUserLoggedIn()
        {
            FormsAuthenticationTicket userTicket = new HttpContextWrapper(HttpContext.Current).GetUmbracoAuthTicket();

            if (userTicket != null)
            {
                IUser currentUser = ApplicationContext.Current.Services.UserService.GetByUsername(userTicket.Name);

                if (!string.IsNullOrEmpty(currentUser.UserType.Alias) &&
                    currentUser.UserType.Alias == "admin")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the settings node.
        /// </summary>
        /// <returns></returns>
        public static IPublishedContent GetSettingsNode()
        {
            SettingsService settingsService = new SettingsService();

            return  settingsService.GetSettingsNode();
        }
        
        /// <summary>
        /// Gets the settings model.
        /// </summary>
        /// <returns></returns>
        public static SettingsModel GetSettingsModel()
        {
            IPublishedContent settingsNode = GetSettingsNode();

            SettingsModel settingsModel = new SettingsModel(settingsNode);

            return settingsModel;
        }

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <returns></returns>
        public static CustomerModel GetCustomerModel()
        {
            SettingsService settingsService = new SettingsService();

            IPublishedContent customerNode = settingsService.GetCustomerNode();

            if (customerNode != null)
            {
                CustomerModel customerModel = new CustomerModel(customerNode);
                return customerModel;
            }

            return null;
        }

        /// <summary>
        /// Gets the page model.
        /// </summary>
        /// <returns></returns>
        public static PageModel GetPageModel(PublishedContentRequest request)
        {
            return new PageModel(request.PublishedContent);
        }
    }
}
