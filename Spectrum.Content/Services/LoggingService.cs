namespace Spectrum.Content.Services
{
    using System;
    using Umbraco.Core.Logging;

    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error<T>(
            string message, 
            Exception exception)
        {
            LogHelper.Error<T>(message, exception);
        }

        /// <summary>
        /// Errors the specified calling type.
        /// </summary>
        /// <param name="callingType">Type of the calling.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(
            Type callingType, 
            string message, 
            Exception exception)
        {
            LogHelper.Error(callingType, message, exception);
        }

        /// <summary>
        /// Informations the specified calling type.
        /// </summary>
        /// <param name="callingType">Type of the calling.</param>
        /// <param name="message">The message.</param>
        public void Info(
            Type callingType,
            string message)
        {
            LogHelper.Info(callingType, message);
        }

        /// <summary>
        /// Informations the specified calling type.
        /// </summary>
        /// <param name="callingType">Type of the calling.</param>
        public void Info(Type callingType)
        {
            LogHelper.Info(callingType, string.Empty);
        }
    }
}
