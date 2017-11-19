namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

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
        /// Gets a value indicating whether [quotes enabled].
        /// </summary>
        public bool QuotesEnabled => this.GetPropertyValue<bool>("quotesEnabled");

        /// <summary>
        /// Gets a value indicating whether [quote email template].
        /// </summary>
        public string QuoteEmailTemplate => this.GetPropertyValue<string>("quoteEmailTemplate");
    }
}
