namespace Spectrum.Content.Correspondence.Manangers
{
    using ContentModels;
    using Mail.Providers;
    using Services;
    using System;
    using System.Collections.Generic;
    using ViewModels;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    
    public class ContactUsManager : IContactUsManager
    {
        /// <summary>
        /// The logging service.
        /// </summary>
        private readonly ILoggingService loggingService;

        /// <summary>
        /// The mail provider.
        /// </summary>
        private readonly IMailProvider mailProvider;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="mailProvider">The mail provider.</param>
        /// <param name="settingsService">The settings service.</param>
        public ContactUsManager(
            ILoggingService loggingService, 
            IMailProvider mailProvider,
            ISettingsService settingsService)
        {
            this.loggingService = loggingService;
            this.mailProvider = mailProvider;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Inserts the contact us.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public string InsertContactUs(
            UmbracoContext umbracoContext, 
            IPublishedContent publishedContent, 
            ContactUsViewModel viewModel)
        {
            ////loggingService.Info(GetType(), "Start");

            IPublishedContent settingsNode = settingsService.GetSettingsNode();

            SettingsModel settingsModel = new SettingsModel(settingsNode);

            string emailAddress = settingsModel.EmailAddress;
            string contactUsEmailAddress = settingsModel.ContactUsEmailAddress;
            
            IPublishedContent customerNode = settingsService.GetCustomerNode();

            if (customerNode != null)
            {
                CustomerModel customerModel = new CustomerModel(customerNode);

                emailAddress = customerModel.EmailAddress;
                contactUsEmailAddress = customerModel.ContactUsEmailAddress;
            }
            
            PageModel pageModel = new PageModel(publishedContent);

            try
            {
                string emailTemplate = pageModel.EmailTemplateName;

                if (string.IsNullOrEmpty(emailTemplate) == false)
                {
                     Dictionary<string, string> dictionairy = new Dictionary<string, string>
                        {
                            {"Name", viewModel.Name},
                            {"EmailAddress", viewModel.EmailAddress},
                            {"Message", viewModel.Message}
                        };
                     
                    mailProvider.SendEmail(
                        umbracoContext,
                        emailTemplate,
                        emailAddress,
                        contactUsEmailAddress,
                        string.Empty,
                        null,
                        dictionairy);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    loggingService.Error(GetType(), "Contact Us Error", ex);
                }
                catch (Exception)
                {
                    // ignored
                }

                return pageModel.ErrorPageUrl;
            }

            return pageModel.NextPageUrl;
        }
    }
}
