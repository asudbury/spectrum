namespace Spectrum.Application.Authentication.Controllers
{
    using Model;
    using Providers;

    public class LoginController
    {
        /// <summary>
        /// The login provider.
        /// </summary>
        private readonly ILoginProvider loginProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="loginProvider">The login provider.</param>
        public LoginController(ILoginProvider loginProvider)
        {
            this.loginProvider = loginProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        public LoginController()
            : this(new LoginProvider())
        {
        }

        /// <summary>
        /// The user has logged in.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginComplete(NotificationModel model)
        {
            loginProvider.LoginComplete(model);
        }

        /// <summary>
        /// The user login has failed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginFailed(NotificationModel model)
        {
            loginProvider.LoginFailed(model);
        }
    }
}
