namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class PageModel : BaseModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.ContentModels.PageModel" /> class.
        /// </summary>
        /// <param name="content">The original content.</param>
        public PageModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets the next page URL.
        /// </summary>
        public string NextPageUrl => GetNiceUrl(GetNodeId("nextPage"));

        /// <summary>
        /// Gets the error page URL.
        /// </summary>
        public string ErrorPageUrl => GetNiceUrl(GetNodeId("errorPage"));

        /// <summary>
        /// Gets the name of the email template.
        /// </summary>
        public string EmailTemplateName => this.GetPropertyValue<string>("emailTemplateName");
    }
}


