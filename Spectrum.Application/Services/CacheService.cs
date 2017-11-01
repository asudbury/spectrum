namespace Spectrum.Application.Services
{
    using System;
    using System.Web;

    public class CacheService : ICacheService
    {
        /// <inheritdoc />
        /// <summary>
        /// Adds the specified o.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">The o.</param>
        /// <param name="key">The key.</param>
        public void Add<T>(T o, string key) where T : class
        {
            HttpContext.Current.Cache.Insert(
                key,
                o,
                null,
                DateTime.Now.AddMinutes(60),
                System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            try
            {
                return (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Clears the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Clear(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        /// <inheritdoc />
        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }
    }
}
