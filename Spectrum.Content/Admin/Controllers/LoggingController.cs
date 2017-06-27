﻿namespace Spectrum.Content.Admin.Controllers
{
    using Services;
    using System;
    using System.Web.Mvc;

    public class LoggingController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        public LoggingController(
            ILoggingService loggingService,
            ISettingsService settingsService) 
            : base(loggingService)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingController"/> class.
        /// </summary>
        public LoggingController()
            : this(new LoggingService(),
                   new SettingsService())
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