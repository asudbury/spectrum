namespace Spectrum.Application.Services
{
    using System;

    public interface ICookieService
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// Gets the value of a cookie with the given key.
        /// </summary>
        /// <typeparam name="T">The type of value the cookie contains.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value converted into type T</returns>
        T GetValue<T>(string key) where T : struct;

        /// <summary>
        /// Sets a cookie that will expire with users browser session.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void SetValue(
            string key, 
            object value);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        void SetValue(
            string key,
            object value,
            DateTime expires);

        /// <summary>
        /// Expires the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Expire(string key);
    }
}
