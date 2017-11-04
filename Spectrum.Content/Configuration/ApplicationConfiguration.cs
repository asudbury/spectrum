namespace Spectrum.Content.Configuration
{
    using Appointments.Models;
    using ContentModels;
    using Extensions;
    using System;
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Security;
    using Umbraco.Web.Routing;
    using System.Web.Security;
    using Services;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Web;

    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Setups this instance.
        /// </summary>
        /// <param name="umbracoApplication">The umbraco application.</param>
        /// <param name="applicationContext">The application context.</param>
        public static void Started(
            UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            LogHelper.Info(typeof(ApplicationConfiguration), "ApplicationStarted");

            DatabaseContext databaseContext = applicationContext.DatabaseContext;

            DatabaseSchemaHelper db = new DatabaseSchemaHelper(
                databaseContext.Database,
                applicationContext.ProfilingLogger.Logger,
                databaseContext.SqlSyntax);

            bool created = db.CreateTableIfNotExist<AppointmentStatusModel>(Spectrum.Content.Constants.Database.AppointmentStatusTableName);

            if (created)
            {
                foreach (AppointmentStatus status in Enum.GetValues(typeof(AppointmentStatus)))
                {
                    databaseContext.Database.Insert(new AppointmentStatusModel
                    {
                        Id = (int) status,
                        Description = status.ToString()
                    });
                }
            }

            db.CreateTableIfNotExist<AppointmentModel>(Spectrum.Content.Constants.Database.AppointmentTableName);
            db.CreateTableIfNotExist<AppointmentAttendeeModel>(Spectrum.Content.Constants.Database.AppointmentAttendeeTableName);
            db.CreateTableIfNotExist<ICalAppointmentModel>(Spectrum.Content.Constants.Database.IcalAppointmentTableName);

            PublishedContentRequest.Prepared += PublishedContentRequestPrepared;
        }

        /// <summary>
        /// Publisheds the content request prepared.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void PublishedContentRequestPrepared(
            object sender,
            EventArgs e)
        {
            PublishedContentRequest request = (PublishedContentRequest) sender;

            //// Check to make sure the request is valid
            if (request == null ||
                request.HasPublishedContent == false)
            {
                return;
            }

            if (IsBackOfficeUserLoggedIn())
            {
                return;
            }
            
            try
            {
                string url = GetRedirectUrl(request);

                if (string.IsNullOrEmpty(url) == false)
                {
                    request.SetRedirect(url);
                }
            }
            catch (Exception exception)
            {
                LogHelper.Error(typeof(ApplicationConfiguration), "ApplicationConfiguration PublishedContentRequestPrepared", exception);
            }
        }

        /// <summary>
        /// Gets the redirect URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private static string GetRedirectUrl(PublishedContentRequest request)
        {
            //// check if the whole site is offline
            
            SettingsModel settingsModel = GetSettingsModel();

            string offlineUrl = settingsModel.OfflineUrl;

            LogHelper.Info(typeof(ApplicationConfiguration), "OfflineUrl=" + offlineUrl);

            if (string.IsNullOrEmpty(offlineUrl))
            {
                return string.Empty;
            }

            string compareOfflineUrl = offlineUrl.Replace("/", string.Empty);

            if (request.Uri.ToString().Contains(compareOfflineUrl))
            {
                return string.Empty;
            }

            if (settingsModel.SiteEnabled == false)
            {
                LogHelper.Info(typeof(ApplicationConfiguration), "SiteEnabled=false");
                return offlineUrl;
            }

            //// now we need to check that customer is enabled!

            CustomerModel customerModel = GetCustomerModel();

            if (customerModel?.CustomerEnabled == false)
            {
                LogHelper.Info(typeof(ApplicationConfiguration), "CustomerEnabled=false");
                return offlineUrl;
            }

            //// now check if we are the customer virtual home page
           
            //// now check if the page has a redirect setup

            PageModel pageModel = GetPageModel(request);

            if (string.IsNullOrEmpty(pageModel.RedirectUrl) == false)
            {
                return pageModel.RedirectUrl;
            }

            return string.Empty;
        }

        /// <summary>
        /// Determines whether [is back office user logged in].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is back office user logged in]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsBackOfficeUserLoggedIn()
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
        /// Gets the settings model.
        /// </summary>
        /// <returns></returns>
        private static SettingsModel GetSettingsModel()
        {
            SettingsService settingsService = new SettingsService();

            IPublishedContent settingsNode = settingsService.GetSettingsNode(UmbracoContext.Current);

            SettingsModel settingsModel = new SettingsModel(settingsNode);

            return settingsModel;
        }

        /// <summary>
        /// Gets the customer enabled.
        /// </summary>
        /// <returns></returns>
        private static CustomerModel GetCustomerModel()
        {
            SettingsService settingsService = new SettingsService();

            IPublishedContent customerNode = settingsService.GetCustomerNode(UmbracoContext.Current);

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
        private static PageModel GetPageModel(PublishedContentRequest request)
        {
            return new PageModel(request.PublishedContent);
        }
    }
}
