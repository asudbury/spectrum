namespace Spectrum.Application.Authentication.Providers
{
    using Model;
    using Repositories;

    /// <summary>
    /// The LoginProvider class.
    /// </summary>
    internal class LoginProvider : ILoginProvider
    {
        /// <summary>
        /// The registration repository.
        /// </summary>
        private readonly ILoginRepository loginRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginProvider" /> class.
        /// </summary>
        /// <param name="loginRepository">The login repository.</param>
        internal LoginProvider(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginProvider"/> class.
        /// </summary>
        internal LoginProvider()
            : this(new LoginRepository())
        {
        }

        /// <summary>
        /// The user has logged in.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginComplete(NotificationModel model)
        {
            loginRepository.LoginComplete(model);
        }

        /// <summary>
        /// The user login has failed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginFailed(NotificationModel model)
        {
            loginRepository.LoginFailed(model);
        }
    }
}
