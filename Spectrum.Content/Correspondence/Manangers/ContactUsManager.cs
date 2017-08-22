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
        /// Initializes a new instance of the <see cref="ContactUsManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="mailProvider">The mail provider.</param>
        public ContactUsManager(
            ILoggingService loggingService, 
            IMailProvider mailProvider)
        {
            this.loggingService = loggingService;
            this.mailProvider = mailProvider;
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
                        viewModel.EmailAddress,
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
        }}
}
