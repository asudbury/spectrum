namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class MenuItemModel : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Umbraco.Core.Models.PublishedContent.PublishedContentModel" /> class with
        /// an original <see cref="T:Umbraco.Core.Models.IPublishedContent" /> instance.
        /// </summary>
        /// <param name="content">The original content.</param>
        public MenuItemModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        public string Icon => this.GetPropertyValue<string>("icon");

        /// <summary>
        /// Gets the text.
        /// </summary>

        public string Text => this.GetPropertyValue<string>("text");

        /// <summary>
        /// Gets the link.
        /// </summary>
        public string Link => GetNiceUrl(this.GetPropertyValue<int>("link"));
    }
}
