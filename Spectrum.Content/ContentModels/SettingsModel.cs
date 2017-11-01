namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class SettingsModel : BaseModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Umbraco.Core.Models.PublishedContent.PublishedContentModel" /> class with
        /// an original <see cref="T:Umbraco.Core.Models.IPublishedContent" /> instance.
        /// </summary>
        /// <param name="content">The original content.</param>
        public SettingsModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets a value indicating whether [site enabled].
        /// </summary>
        public bool SiteEnabled => this.GetPropertyValue<bool>("siteEnabled");
    }
}
