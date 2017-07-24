namespace Spectrum.Application.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// Adds the specified o.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">The o.</param>
        /// <param name="key">The key.</param>
        void Add<T>(T o, string key) where T : class;

        T Get<T>(string key) where T : class;

        /// <summary>
        /// Clears the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Clear(string key);

        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Exists(string key);
    }
}
