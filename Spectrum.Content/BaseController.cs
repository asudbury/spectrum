namespace Spectrum.Content
{
    using Services;
    using System;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web.Mvc;

    public class BaseController : SurfaceController
    {
        /// <summary>
        /// The logging service.
        /// </summary>
        protected readonly ILoggingService LoggingService;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        public BaseController(ILoggingService loggingService)
        {
            LoggingService = loggingService;
        }

        /// <summary>
        /// Gets the node identifier.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetNodeId()
        {
            return Content(CurrentPage.Id.ToString());
        }

        /// <summary>
        /// Gets the content by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IPublishedContent GetContentById(int id)
        {
            return Umbraco.TypedContent(id);
        }

        /// <summary>
        /// Gets the content by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IPublishedContent GetContentById(string id)
        {
            return GetContentById(Convert.ToInt32(id));
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

        /// <summary>
        /// Throws the access denied exception.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void ThrowAccessDeniedException(string message)
        {
            ApplicationException applicationException = new ApplicationException(message);
            LoggingService.Error(GetType(), message, applicationException);
            throw applicationException;
        }
    }
}

