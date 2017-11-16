namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;

    public class QuoteSettingsModel : BaseModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Umbraco.Core.Models.PublishedContent.PublishedContentModel" /> class with
        /// an original <see cref="T:Umbraco.Core.Models.IPublishedContent" /> instance.
        /// </summary>
        /// <param name="content">The original content.</param>
        public QuoteSettingsModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets the payments enabled.
        /// </summary>
        public bool QuotesEnabled => false;
    }
}
