using System.Web.Security;
using Spectrum.Content.Services;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Umbraco.Web;

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

            bool created = db.CreateTableIfNotExist<AppointmentStatusModel>(Content.Constants.Database.AppointmentStatusTableName);

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

            db.CreateTableIfNotExist<AppointmentModel>(Content.Constants.Database.AppointmentTableName);
            db.CreateTableIfNotExist<AppointmentAttendeeModel>(Content.Constants.Database.AppointmentAttendeeTableName);
            db.CreateTableIfNotExist<ICalAppointmentModel>(Content.Constants.Database.IcalAppointmentTableName);

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
                SettingsService settingsService = new SettingsService();

                IPublishedContent settingsNode = settingsService.GetSettingsNode(UmbracoContext.Current);

                if (settingsNode == null)
                {
                    return;
                }

                SettingsModel settingsModel = new SettingsModel(settingsNode);

                if (settingsModel.SiteEnabled == false)
                {
                    LogHelper.Info(typeof(ApplicationConfiguration), "SiteEnabled=false");

                    string offlineUrl = settingsModel.OfflineUrl;

                    LogHelper.Info(typeof(ApplicationConfiguration), "OfflineUrl=" + offlineUrl);

                    string compareOfflineUrl = offlineUrl.Replace("/", string.Empty);

                    if (request.Uri.ToString().Contains(compareOfflineUrl))
                    {
                        return;
                    }

                    if (string.IsNullOrEmpty(offlineUrl) == false &&
                        offlineUrl != "#")
                    {
                        request.SetRedirect(offlineUrl);
                    }
                }
            }
            catch (Exception exception)
            {
                LogHelper.Error(typeof(ApplicationConfiguration), "ApplicationConfiguration PublishedContentRequestPrepared", exception);
            }
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
    }
}
