namespace Spectrum.Model
{
    using System.Collections.Specialized;
    using System.Net.Mail;

    public class EmailRequest
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public MailMessage Message { get; set; }

        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        public ListDictionary Tokens { get; set; }
    }
}
