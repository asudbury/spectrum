namespace Spectrum.Content.Authentication.Managers
{
    using Application.Services;
    using ContentModels;
    using Services;
    using System;
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class LoginManager : ILoginManager
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The cookie service.
        /// </summary>
        private readonly ICookieService cookieService;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;


        /// <summary>
        /// Initializes a new instance of the <see cref="LoginManager" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="cookieService">The cookie service.</param>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="userService">The user service.</param>
        public LoginManager(
            ISettingsService settingsService,
            ICookieService cookieService, 
            IEncryptionService encryptionService,
            IUserService userService)
        {
            this.settingsService = settingsService;
            this.cookieService = cookieService;
            this.encryptionService = encryptionService;
            UserService = userService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the user service.
        /// </summary>
        public IUserService UserService { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the login view model.
        /// </summary>
        /// <returns></returns>
        public LoginViewModel GetLoginViewModel()
        {
            string emailAddress = cookieService.GetValue(AuthenticationConstants.EmailAddress);
            string password = cookieService.GetValue(AuthenticationConstants.Password);
            string rememberMe = cookieService.GetValue(AuthenticationConstants.RememberMe);

            LoginViewModel viewModel = new LoginViewModel();

            if (string.IsNullOrEmpty(emailAddress) == false)
            {
                viewModel.EmailAddress = encryptionService.DecryptString(emailAddress);
            }

            if (string.IsNullOrEmpty(password) == false)
            {
                viewModel.Password = encryptionService.DecryptString(password);
            }

            if (string.IsNullOrEmpty(rememberMe) == false)
            {
                bool remember = Convert.ToBoolean(encryptionService.DecryptString(rememberMe));

                viewModel.RememberMe = remember;
            }

            return viewModel;
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether [is user logged in].
        /// </summary>
        public bool IsUserLoggedIn()
        {
            return UserService.IsUserLoggedIn();
        }

        /// <inheritdoc />
        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool Login(
            string userName, 
            string password)
        {
            return UserService.Login(userName, password);
        }

        /// <inheritdoc />
        /// <summary>
        /// Logs the out.
        /// </summary>
        public void LogOut()
        {
            UserService.Logout();
        }

        /// <summary>
        /// Sets the cookies.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="isLocalHost">if set to <c>true</c> [is local host].</param>
        /// <inheritdoc />
        public void SetCookies(
            LoginViewModel viewModel,
                bool isLocalHost)
        {
            FormsAuthentication.SetAuthCookie(viewModel.EmailAddress, viewModel.RememberMe);

            if (viewModel.RememberMe)
            {
                string encryptedEmailAddress = encryptionService.EncryptString(viewModel.EmailAddress);
                cookieService.SetValue(AuthenticationConstants.EmailAddress, encryptedEmailAddress);

                string encryptedRememberMe = encryptionService.EncryptString(viewModel.RememberMe.ToString());
                cookieService.SetValue(AuthenticationConstants.RememberMe, encryptedRememberMe);

                if (isLocalHost)
                {
                    string encryptedPassword = encryptionService.EncryptString(viewModel.Password);
                    cookieService.SetValue(AuthenticationConstants.Password, encryptedPassword);
                }
            }

            else
            {
                cookieService.Expire(AuthenticationConstants.EmailAddress);
                cookieService.Expire(AuthenticationConstants.Password);
                cookieService.Expire(AuthenticationConstants.RememberMe);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the return URL.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        public string GetReturnUrl(string emailAddress)
        {
            string returnUrl = string.Empty;

            IMember member = UserService.GetUser(emailAddress);

            UserService.UpdateLoginStatus(member);

            string role = UserService.GetDefaultRole(member.Username);

            if (!string.IsNullOrEmpty(role))
            {
                IPublishedContent menuNode = settingsService.GetMenu(UmbracoContext.Current, role);

                if (menuNode != null)
                {
                    MenuModel menuModel = new MenuModel(menuNode);

                    returnUrl = menuModel.LandingPage;
                }
            }

            return returnUrl;
        }
    }
}
