namespace Spectrum.Content.Repositories
{
    using Scorchio.Services;
    using System;

    public abstract class BaseRepository
    {
        /// <summary>
        /// The cache service.
        /// </summary>
        private readonly ICacheService cacheService;

        /// <summary>
        /// The key
        /// </summary>
        protected string Key = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="cacheService">The cache service.</param>
        protected BaseRepository(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        /// <summary>
        /// Adds the specified o.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">The o.</param>
        public void Add<T>(T o) where T : class
        {
            cacheService.Add(o, GetKey());
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class
        {
            return cacheService.Get<T>(GetKey());
        }

        /// <summary>
        /// Clears the specified key.
        /// </summary>
        public void Clear()
        {
            cacheService.Clear(GetKey());
        }

        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            return cacheService.Exists(GetKey());
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <returns></returns>
        internal string GetKey()
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new ApplicationException("Transactions Cache Key Not Set.");
            }

            return Key;
        }
    }
}
