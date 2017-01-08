namespace Spectrum.Application.Authentication.Controllers
{
    using Model;
    using Providers;

    public class PasswordController
    {
        /// <summary>
        /// The password provider.
        /// </summary>
        private readonly IPasswordProvider passwordProvider;

        public PasswordController(IPasswordProvider passwordProvider)
        {
            this.passwordProvider = passwordProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordController"/> class.
        /// </summary>
        public PasswordController()
            : this(new PasswordProvider())
        {
            
        }
        /// <summary>
        /// Password reset requested.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetRequested(NotificationModel model)
        {
            passwordProvider.ResetRequested(model);
        }

        /// <summary>
        /// Password Reset Completed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetCompleted(NotificationModel model)
        {
            passwordProvider.ResetRequested(model);    
        }
    }
}
