namespace Spectrum.Content.Authentication.Managers
{
    using Content.Services;
    using ContentModels;
    using Services;
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
        private readonly ILoginCookieService loginCookieService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginManager" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="loginCookieService">The login cookie service.</param>
        /// <param name="userService">The user service.</param>
        public LoginManager(
            ISettingsService settingsService,
            ILoginCookieService loginCookieService, 
            IUserService userService)
        {
            this.settingsService = settingsService;
            this.loginCookieService = loginCookieService;
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
            return new LoginViewModel
            {
                EmailAddress = loginCookieService.GeEmailAdress(),
                Password = loginCookieService.GetPassword(),
                RememberMe = loginCookieService.GetRememberMe()
            };
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

            loginCookieService.SetCookies(
                UmbracoContext.Current, 
                isLocalHost,
                viewModel.EmailAddress, 
                viewModel.RememberMe, 
                viewModel.Password);
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
