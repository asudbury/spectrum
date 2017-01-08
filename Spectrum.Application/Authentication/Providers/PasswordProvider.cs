namespace Spectrum.Application.Authentication.Providers
{
    using Model;
    using Repositories;

    /// <summary>
    /// The PasswordProvider class.
    /// </summary>
    internal class PasswordProvider : IPasswordProvider
    {
        /// <summary>
        /// The password repository.
        /// </summary>
        private readonly IPasswordRepository passwordRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordProvider" /> class.
        /// </summary>
        /// <param name="passwordRepository">The password repository.</param>
        internal PasswordProvider(IPasswordRepository passwordRepository)
        {
            this.passwordRepository = passwordRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordProvider"/> class.
        /// </summary>
        internal PasswordProvider()
            : this(new PasswordRepository())
        {
        }

        /// <summary>
        /// Password reset requested.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetRequested(NotificationModel model)
        {
            passwordRepository.ResetRequested(model);
        }

        /// <summary>
        /// Password Reset Completed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetCompleted(NotificationModel model)
        {
            passwordRepository.ResetCompleted(model);
        }
    }
}
