namespace Spectrum.Content.ContentModels
{
    using System.Collections.Generic;
    using System.Net.Mail;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class MailTemplateModel : BaseModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.ContentModels.MailTemplateModel" /> class.
        /// </summary>
        /// <param name="content">The original content.</param>
        public MailTemplateModel(IPublishedContent content)
          : base(content)
        {
            Attachments = new List<Attachment>();
            Subject = this.GetPropertyValue<string>("subject");
        }

        /// <summary>
        /// Gets a value indicating whether [surpress send email].
        /// </summary>
        public bool SurpressSendEmail => this.GetPropertyValue<bool>("surpressSendEmail");

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the blind copy.
        /// </summary>
        public string BlindCopy { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text => this.GetPropertyValue<string>("text");

        /// <summary>
        /// Gets a value indicating whether this instance is HTML.
        /// </summary>
        public bool IsHtml => this.GetPropertyValue<bool>("isHtml");

        /// <summary>
        /// Gets or sets the tokenized text.
        /// </summary>
        public string TokenizedText { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    } 
}
