namespace Spectrum.Content.Services
{
    using System;

    public interface ILoggingService
    {
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error<T>(
            string message, 
            Exception exception);

        /// <summary>
        /// Errors the specified calling type.
        /// </summary>
        /// <param name="callingType">Type of the calling.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error(
            Type callingType,
            string message,
            Exception exception);

        /// <summary>
        /// Informations the specified calling type.
        /// </summary>
        /// <param name="callingType">Type of the calling.</param>
        /// <param name="message">The message.</param>
        void Info(
            Type callingType,
            string message);

        /// <summary>
        /// Informations the specified calling type.
        /// </summary>
        /// <param name="callingType">Type of the calling.</param>
        void Info(Type callingType);
    }
}
