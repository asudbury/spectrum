using System;

namespace Spectrum.Content.Mail.Providers
{
    using Content.Services;
    using ContentModels;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Net.Mail;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class MailProvider : IMailProvider
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The mail service.
        /// </summary>
        private readonly IMailService mailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailProvider"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="mailService">The mail service.</param>
        public MailProvider(
            ISettingsService settingsService,
            IMailService mailService)
        {
            this.settingsService = settingsService;
            this.mailService = mailService;
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="emailTemplateName">Name of the email template.</param>
        /// <param name="to">To.</param>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            string emailTemplateName, 
            string to,
            Attachment attachment)
        {
            return SendEmail(umbracoContext, emailTemplateName, to, attachment, null);
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="emailTemplateName">Name of the email template.</param>
        /// <param name="to">To.</param>
        /// <param name="attachment">The attachment.</param>
        /// <param name="replacementTokens">The replacement tokens.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            string emailTemplateName, 
            string to, 
            Attachment attachment,
            Dictionary<string, string> replacementTokens)
        {
            IPublishedContent content = settingsService.GetMailTemplate(
                                            umbracoContext, 
                                            emailTemplateName);
            if (content == null)
            {
                throw new ApplicationException("Mail Template " + emailTemplateName + " not defined");
            }

            MailTemplateModel model = new MailTemplateModel(content);

            string newText = model.Text;

            if (replacementTokens != null)
            {
                foreach (KeyValuePair<string, string> token in replacementTokens)
                {
                    newText = newText.Replace("$" + token.Key, token.Value);
                }
            }

            model.TokenizedText = newText;

            if (attachment != null)
            {
                model.Attachments.Add(attachment);    
            }

            return mailService.SendEmail(to, model);
        }
    }
}
