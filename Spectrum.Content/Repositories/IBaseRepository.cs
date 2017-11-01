namespace Spectrum.Content.Repositories
{
    using Umbraco.Web;

    public interface IBaseRepository
    {
        /// <summary>
        /// Sets the key.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        void SetKey(UmbracoContext umbracoContext);

        /// <summary>
        /// Adds the specified o.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">The o.</param>
        void Add<T>(T o) where T : class;

        T Get<T>() where T : class;

        /// <summary>
        /// Clears the specified key.
        /// </summary>
        void Clear();

        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <returns></returns>
        bool Exists();
    }
}
