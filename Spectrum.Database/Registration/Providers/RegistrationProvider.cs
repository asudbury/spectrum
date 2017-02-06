namespace Spectrum.Database.Registration.Providers
{
    using Repositories;

    /// <summary>
    /// The RegistrationProvider class.
    /// </summary>
    /// <seealso cref="IRegistrationProvider" />
    internal class RegistrationProvider : IRegistrationProvider
    {
        /// <summary>
        /// The registration repository.
        /// </summary>
        private readonly IRegistrationRepository registrationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationProvider"/> class.
        /// </summary>
        /// <param name="registrationRepository">The registration repository.</param>
        internal RegistrationProvider(IRegistrationRepository registrationRepository)
        {
            this.registrationRepository = registrationRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationProvider"/> class.
        /// </summary>
        internal RegistrationProvider()
            : this(new RegistrationRepository())
        {
        }
        
        /// <summary>
        /// Bootstraps this instance.
        /// </summary>
        public void Bootstrap()
        {
            registrationRepository.Bootstrap();
        }
    }
}
