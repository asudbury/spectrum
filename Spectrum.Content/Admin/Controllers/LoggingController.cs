namespace Spectrum.Content.Admin.Controllers
{
    using Services;
    using System;
    using System.Web.Mvc;
    using Umbraco.Web;

    public class LoggingController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="loggingService"></param>
        public LoggingController(
            ILoggingService loggingService) 
            : base(loggingService)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loggingService"></param>
        public LoggingController(
            UmbracoContext context, 
            ILoggingService loggingService) 
            : base(context, 
                   loggingService)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingController"/> class.
        /// </summary>
        public LoggingController()
            : this(new LoggingService())
        {
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        [HttpPost]
        public void LogInfo(string message)
        {
            LoggingService.Info(GetType(), message);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        [HttpPost]
        public void LogError(string message)
        {
            LoggingService.Error(GetType(), message, new Exception());
        }
    }
}
