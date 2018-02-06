using System.Web;

namespace Spectrum.Content.Mail.Providers
{
    using Content.Services;
    using ContentModels;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System;
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
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="blindCopy">The blind copy.</param>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            string emailTemplateName,
            string from,
            string to,
            string blindCopy,
            Attachment attachment)
        {
            return SendEmail(
                umbracoContext, 
                emailTemplateName, 
                from, 
                to, 
                blindCopy, 
                attachment, 
                null);
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="emailTemplateName">Name of the email template.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="blindCopy">The blind copy.</param>
        /// <param name="attachment">The attachment.</param>
        /// <param name="replacementTokens">The replacement tokens.</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Mail Template " + emailTemplateName + " not defined</exception>
        /// <inheritdoc />
        public MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            string emailTemplateName,
            string from,
            string to,
            string blindCopy,
            Attachment attachment,
            Dictionary<string, string> replacementTokens)
        {
            IPublishedContent content = settingsService.GetMailTemplate(emailTemplateName);

            if (content == null)
            {
                throw new ApplicationException("Mail Template " + emailTemplateName + " not found");
            }

            MailTemplateModel model = new MailTemplateModel(content);

            string newText = model.Text;

            //// replace the escaped characters!

            newText = newText
                .Replace("&lt;", "<")
                .Replace("&amp;", "&")
                .Replace("&gt;", ">")
                .Replace("&quot;", "\"")
                .Replace("&apos;", "'");
            
            if (model.IsHtml)
            {
                //// we need to get the template if one has been setup!

                if (model.TemplateId > 0)
                {
                    UmbracoHelper umbracoHelper = new UmbracoHelper(umbracoContext);

                    IHtmlString htmlString = umbracoHelper.RenderTemplate(model.Id, model.TemplateId);

                    if (string.IsNullOrEmpty(htmlString.ToString()) == false)
                    {
                        newText = htmlString.ToString().Replace("$Content", newText);
                    }
                }
            }

            if (replacementTokens != null)
            {
                foreach (KeyValuePair<string, string> token in replacementTokens)
                {
                    newText = newText.Replace("$" + token.Key, token.Value);
                    model.Subject = model.Subject.Replace("$" + token.Key, token.Value);
                }
            }

            model.TokenizedText = newText;

            if (attachment != null)
            {
                model.Attachments.Add(attachment);
            }

            model.From = from;
            model.To = to;
            model.BlindCopy = blindCopy;

            return mailService.SendEmail(model);
        }
    }
}
