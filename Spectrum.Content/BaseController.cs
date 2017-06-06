namespace Spectrum.Content
{
    using Services;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;

    public class BaseController : SurfaceController
    {
        /// <summary>
        /// The logging service.
        /// </summary>
        protected readonly ILoggingService LoggingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public BaseController(ILoggingService loggingService)
        {
            LoggingService = loggingService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public BaseController(
            UmbracoContext context,
            ILoggingService loggingService)
            :base(context)
        {
            LoggingService = loggingService;
        }

        /// <summary>
        /// Gets the page URL.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        public string GetPageUrl(int? nodeId)
        {
            string url = string.Empty;

            if (nodeId.HasValue)
            {
                url = Umbraco.TypedContent(nodeId).Url;
            }

            return url;
        }
    }
}

