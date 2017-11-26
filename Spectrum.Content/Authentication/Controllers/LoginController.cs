namespace Spectrum.Content.Authentication.Controllers
{
    using Managers;
    using Services;
    using System;
    using System.Web.Mvc;
    using ViewModels;

    public class LoginController : BaseController
    {
        /// <summary>
        /// The login manager.
        /// </summary>
        private readonly ILoginManager loginManager;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Authentication.Controllers.LoginController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="loginManager">The login manager.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <inheritdoc />
        public LoginController(
            ILoggingService loggingService,
            ILoginManager loginManager,
            ISettingsService settingsService)
            : base(loggingService)
        {
            this.loginManager = loginManager;
            this.settingsService = settingsService;
            this.loginManager.UserService.MemberService = Services.MemberService;
        }

        /// <summary>
        /// Gets the login.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetLogin()
        {
            LoginViewModel viewModel = loginManager.GetLoginViewModel();

            return PartialView("Partials/Spectrum/Membership/Login", viewModel);
        }

        /// <summary>
        /// Handles the login.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleLogin(LoginViewModel viewModel)
        {
            LoggingService.Info(GetType(), "HandleLogin");

            TempData["Status"] = string.Empty;

            if (!ModelState.IsValid)
            {
                return PartialView("Login", viewModel);
            }

            if (loginManager.IsUserLoggedIn())
            {
                LoggingService.Info(GetType(), "User LoggedIn");
                return Redirect("/");
            }

            try
            {
                bool result = loginManager.Login(viewModel.EmailAddress, viewModel.Password);

                if (result)
                {
                    LoggingService.Info(GetType(), "Successful Log In");

                    settingsService.ClearCache();

                    loginManager.SetCookies(viewModel, Request.IsLocal);

                    viewModel.ReturnUrl = loginManager.GetReturnUrl(viewModel.EmailAddress);

                    return !string.IsNullOrEmpty(viewModel.ReturnUrl) ? new RedirectResult(viewModel.ReturnUrl) 
                                                                      : new RedirectResult("/");
                }

                LoggingService.Info(GetType(), "Insuccessful Log In");

                TempData["Status"] = "Invalid Log-in Credentials";
                return RedirectToCurrentUmbracoPage();
            }
            catch (Exception ex)
            {
                LoggingService.Error(GetType(), "Login Error", ex);

                TempData["Status"] = "Could not login";
                return RedirectToCurrentUmbracoPage();
            }
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Logout()
        {
            LoggingService.Info(GetType(), "Logout");

            if (loginManager.IsUserLoggedIn())
            {
                settingsService.ClearCache();
                TempData.Clear();
                Session.Clear();
                loginManager.LogOut();
            }

            return Content("/");
        }
    }
}
