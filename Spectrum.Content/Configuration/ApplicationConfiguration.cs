using Spectrum.Content.Invoices.Models;
using Spectrum.Content.Quotes.Models;

namespace Spectrum.Content.Configuration
{
    using Appointments.Models;
    using ContentModels;
    using Extensions;
    using Payments.Models;
    using System;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Persistence;
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

            LogHelper.Info(typeof(ApplicationConfiguration), "ApplicationStarted Started");

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

            db.CreateTableIfNotExist<TransactionModel>(Content.Constants.Database.TransactionsTableName);

            db.CreateTableIfNotExist<InvoiceModel>(Content.Constants.Database.InvoiceTableName);
            db.CreateTableIfNotExist<QuoteModel>(Content.Constants.Database.QuoteTableName);

            PublishedContentRequest.Prepared += PublishedContentRequestPrepared;

            LogHelper.Info(typeof(ApplicationConfiguration), "ApplicationStarted Ended");
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
            LogHelper.Info(typeof(ApplicationConfiguration), "PublishedContentRequestPrepared Started");

            PublishedContentRequest request = (PublishedContentRequest) sender;

            //// Check to make sure the request is valid
            if (request == null ||
                request.HasPublishedContent == false)
            {
                return;
            }

            if (ConfigurationHelper.IsBackOfficeUserLoggedIn())
            {
                return;
            }
            
            try
            {
                if (request.PublishedContent != null)
                {
                    string url = GetRedirectUrl(request);

                    if (string.IsNullOrEmpty(url) == false)
                    {
                        request.SetRedirect(url);
                    }
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
            LogHelper.Info(typeof(ApplicationConfiguration), "GetRedirectUrl Started");

            //// check if the whole site is offline

            SettingsModel settingsModel = ConfigurationHelper.GetSettingsModel();

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

            CustomerModel customerModel = ConfigurationHelper.GetCustomerModel();

            if (customerModel?.CustomerEnabled == false)
            {
                LogHelper.Info(typeof(ApplicationConfiguration), "CustomerEnabled=false");
                return offlineUrl;
            }

            //// now check if the page has a redirect setup

            PageModel pageModel = ConfigurationHelper.GetPageModel(request);

            if (string.IsNullOrEmpty(pageModel.RedirectUrl) == false)
            {
                return pageModel.RedirectUrl;
            }

            LogHelper.Info(typeof(ApplicationConfiguration), "GetRedirectUrl Ended");

            return string.Empty;
        }
    }
}
