namespace Spectrum.Content.Mail.Providers
{
    using Models;
    using System.Collections.Generic;
    using Umbraco.Web;

    public interface IMailProvider
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="emailTemplateUrl">The email template URL.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            string emailTemplateUrl, 
            string to);

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="emailTemplateUrl">The email template URL.</param>
        /// <param name="to">To.</param>
        /// <param name="replacementTokens">The replacement tokens.</param>
        /// <returns></returns>
        MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            string emailTemplateUrl, 
            string to, 
            Dictionary<string, string> replacementTokens);
    }
}
