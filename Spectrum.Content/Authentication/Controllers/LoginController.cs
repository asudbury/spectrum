namespace Spectrum.Content.Authentication.Controllers
{
    using ContentModels;
    using Services;
    using System;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class LoginController : BaseController
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="userService">The user service.</param>
        public LoginController(
            ILoggingService loggingService,
            ISettingsService settingsService,
            IUserService userService)
            : base(loggingService)
        {
            this.userService = userService;
            this.userService.MemberService = Services.MemberService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        public LoginController() : 
            this(new LoggingService(), 
                 new SettingsService(), 
                 new UserService())
        {
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

            if (userService.IsUserLoggedIn())
            {
                LoggingService.Info(GetType(), "User LoggedIn");
                return Redirect("/");
            }

            try
            {
                bool result = userService.Login(viewModel.EmailAddress, viewModel.Password);

                if (result)
                {
                    LoggingService.Info(GetType(), "Successful Log In");

                    IMember member = userService.GetUser(viewModel.EmailAddress);

                    userService.UpdateLoginStatus(member);

                    string role = userService.GetDefaultRole(member.Username);

                    if (!string.IsNullOrEmpty(role))
                    { 
                        IPublishedContent menuNode = settingsService.GetMenu(UmbracoContext.Current, role);

                        if (menuNode != null)
                        { 
                            MenuModel menuModel = new MenuModel(menuNode);

                            viewModel.ReturnUrl = menuModel.LandingPage;
                        }
                    }

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
        [HttpGet]
        public ActionResult Logout()
        {
            LoggingService.Info(GetType(), "Logout");

            if (userService.IsUserLoggedIn())
            {
                TempData.Clear();
                Session.Clear();
                userService.Logout();
            }

            return Content("/");
        }
    }
}
