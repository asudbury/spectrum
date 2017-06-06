namespace Spectrum.Content
{
    using Services;
    using System;
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
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>the url</returns>
        public string GetPageUrl(
            string propertyName)
        {
            string url = string.Empty;

            if (CurrentPage.GetProperty(propertyName) != null)
            {
                int nodeId = Convert.ToInt32(CurrentPage.GetProperty(propertyName).DataValue);
                url = Umbraco.TypedContent(nodeId).Url;
            }

            return url;
        }
    }
}

