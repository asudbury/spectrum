namespace Spectrum.Application.Registration.Providers
{
    using Core.Services;
    using Model.Registration;
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
            : this(new RegistrationRepository(new DatabaseService()))
        {
        }

        /// <summary>
        /// The user has been registered.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserRegistered(RegisterModel model)
        {
            registrationRepository.UserRegistered(model);
        }
    }
}
