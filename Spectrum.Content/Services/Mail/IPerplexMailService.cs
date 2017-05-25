namespace Spectrum.Content.Services.Mail
{
    using System.Collections.Generic;

    public interface IPerplexMailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="to">To.</param>
        void SendEmail(
            int nodeId,
            string to);

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="to">To.</param>
        /// <param name="tags">The tags.</param>
        void SendEmail(
            int nodeId,
            string to,
            IDictionary<string, string> tags);
    }
}
