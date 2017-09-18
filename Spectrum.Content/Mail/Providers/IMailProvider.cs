namespace Spectrum.Content.Mail.Providers
{
    using Models;
    using System.Collections.Generic;
    using System.Net.Mail;
    using Umbraco.Web;

    public interface IMailProvider
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="emailTemplateName">Name of the email template.</param>
        /// <param name="to">To.</param>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            string emailTemplateName, 
            string to,
            Attachment attachment);

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="emailTemplateName">Name of the email template.</param>
        /// <param name="to">To.</param>
        /// <param name="replacementTokens">The replacement tokens.</param>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            string emailTemplateName, 
            string to, 
            Dictionary<string, string> replacementTokens,
            Attachment attachment);
    }
}
