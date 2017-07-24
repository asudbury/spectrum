namespace Spectrum.Application.Services
{
    using System;
    using System.Globalization;
    using System.Web;

    public class CookieService : ICookieService
    {
        /// <summary>
        /// The http request.
        /// </summary>
        private readonly HttpRequestBase request;

        /// <summary>
        /// The http response.
        /// </summary>
        private readonly HttpResponseBase response;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CookieService"/> class.
        /// </summary>
        public CookieService()
        {
            HttpContextWrapper httpContext = new HttpContextWrapper(HttpContext.Current);
            request = httpContext.Request;
            response = httpContext.Response;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CookieService"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        public CookieService(
            HttpRequestBase request,
            HttpResponseBase response)
        {
            this.response = response;
            this.request = request;
        }
        
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            HttpCookie cookie = request.Cookies[key];
            return cookie?.Value;
        }

        /// <summary>
        /// Gets the value of a cookie with the given key.
        /// </summary>
        /// <typeparam name="T">The type of value the cookie contains.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value converted into type T</returns>
        public T GetValue<T>(string key) where T : struct
        {
            string val = GetValue(key);

            if (val == null)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(val, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Sets a cookie that will expire with users browser session.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetValue(
            string key, 
            object value)
        {
            SetValue(key, value, DateTime.MinValue);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        public void SetValue(
            string key, 
            object value, 
            DateTime expires)
        {
            string stringValue = value.ToString();

            HttpCookie cookie = new HttpCookie(key, stringValue) { Expires = expires };
            response.Cookies.Set(cookie);
        }

        /// <summary>
        /// Expires the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Expire(string key)
        {
            response.SetCookie(new HttpCookie(key) { Expires = DateTime.Today.AddDays(-1) });
        }
    }
}