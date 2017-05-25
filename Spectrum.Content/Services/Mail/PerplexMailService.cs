namespace Spectrum.Content.Services
{
    using Mail;
    using PerplexMail;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;

    public class PerplexMailService : IPerplexMailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="to">To.</param>
        public void SendEmail(
            int nodeId,
            string to)
        {
            Email.SendUmbracoEmail(nodeId,null, null, null, null, new MailAddressCollection { to }, null, null);
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="to">To.</param>
        /// <param name="tags">The tags.</param>
        public void SendEmail(
            int nodeId, 
            string to,
            IDictionary<string, string> tags)
        {
            List<EmailTag> emailTags = tags.Select(keyValuePair => new EmailTag(keyValuePair.Key, keyValuePair.Value)).ToList();

            Email.SendUmbracoEmail(nodeId, emailTags, null, null, null, new MailAddressCollection { to }, null, null);
        }
    }
}
