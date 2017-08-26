namespace Spectrum.Content.Mail.Models
{
    using System.Net.Mime;

    public class MailAttachmentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailAttachmentModel"/> class.
        /// </summary>
        public MailAttachmentModel()
        {
            //// setup for ical by default!
            FileName = "event.ics";
            MimeType = new System.Net.Mime.ContentType("text/calendar");
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// Gets or sets the data.
        /// </summary>

        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the type of the MIME.
        /// </summary>
        public ContentType MimeType { get; set; }
    }
}
