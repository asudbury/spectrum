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
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            int nodeId, 
            string to);

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="to">To.</param>
        /// <param name="replacementTokens">The replacement tokens.</param>
        /// <returns></returns>
        MailResponse SendEmail(
            UmbracoContext umbracoContext, 
            int nodeId, 
            string to, 
            Dictionary<string, string> replacementTokens);
    }
}
