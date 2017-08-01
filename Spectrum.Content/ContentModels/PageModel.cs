namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;

    public class PageModel : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageModel"/> class.
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
        /// Gets the email template URL.
        /// </summary>
        public string EmailTemplateUrl => GetNiceUrl(GetNodeId("emailTemplate"));
    }
}
