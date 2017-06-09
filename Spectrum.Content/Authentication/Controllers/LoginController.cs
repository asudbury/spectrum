namespace Spectrum.Content.Authentication.Controllers
{
    using Services;
    using System;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using ViewModels;

    public class LoginController : BaseController
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="userService">The user service.</param>
        public LoginController(
            ILoggingService loggingService,
            IUserService userService)
            : base(loggingService)
        {
            this.userService = userService;
            this.userService.MemberService = Services.MemberService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        public LoginController() : 
            this(new LoggingService(), 
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

                    userService.UpdateLoginStatus( member);

                    return new RedirectResult(viewModel.ReturnUrl);
                }

                LoggingService.Info(GetType(), "Insuccessful Log In");

                ModelState.AddModelError("LoginForm.", "Invalid details");
                return PartialView("Login", viewModel);
            }
            catch (Exception ex)
            {
                LoggingService.Error(GetType(), "Login Error", ex);
                ModelState.AddModelError("LoginForm.", "Error: " + ex);
                return PartialView("Login", viewModel);
            }
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            LoggingService.Info(GetType(), "Logout");

            if (userService.IsUserLoggedIn())
            {
                Session.Clear();
                userService.Logout();
            }

            return Redirect("/");
        }
    }
}
