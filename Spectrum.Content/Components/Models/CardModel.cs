namespace Spectrum.Content.Components.Models
{
    using ContentModels;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class CardModel : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardModel"/> class.
        /// </summary>
        /// <param name="content">The original content.</param>
        /// <inheritdoc />
        public CardModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title => this.GetPropertyValue<string>("title");

        /// <summary>
        /// Gets the image 
        /// </summary>
        public string Image
        {
            get
            {
                int nodeId = this.GetPropertyValue<int>("image");
                return GetMediaUrl(nodeId);
            }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text => this.GetPropertyValue<string>("text");
        
        /// <summary>
        /// Gets the primary link.
        /// </summary>
        public string PrimaryLink => GetNiceUrl(this.GetPropertyValue<int>("primaryLink"));

        /// <summary>
        /// Gets the primary link text.
        /// </summary>
        public string PrimaryLinkText => this.GetPropertyValue<string>("primaryLinkText");

        /// <summary>
        /// Gets or sets the secondary link.
        /// </summary>
        public string SecondaryLink => GetNiceUrl(this.GetPropertyValue<int>("secondaryLink"));

        /// <summary>
        /// Gets the secondary link text.
        /// </summary>
        public string SecondaryLinkText => this.GetPropertyValue<string>("secondaryLinkText");

        /// <summary>
        /// Gets or sets the display rule.
        /// </summary>
        public string DisplayRule => this.GetPropertyValue<string>("displayRule");
    }
}
