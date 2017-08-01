namespace Spectrum.Content.Mail.Models
{
    public class MailResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MailResponse"/> is sent.
        /// </summary>
        public bool Sent { get; set; }

        /// <summary>
        /// Gets or sets to email address.
        /// </summary>
        public string ToEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MailResponse"/> is surpressed.
        /// </summary>
        public bool Surpressed { get; set; }

        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// Gets or sets the name of the attachment file.
        /// </summary>
        public string AttachmentFileName { get; set; }

        /// <summary>
        /// Gets or sets the calendar event data.
        /// </summary>
        public string AttachmentData { get; set; }

        /// <summary>
        /// Gets or sets the type of the attachment MIME.
        /// </summary>
        public string AttachmentMimeType { get; set; }
    }
}
