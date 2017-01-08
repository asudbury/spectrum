namespace Spectrum.Content.Authentication.Controllers
{
    using Services;
    using System;
    using System.Web.Mvc;
    using ViewModels;
    using Umbraco.Core.Models;
    using Umbraco.Web.Mvc;

    public class LoginController : SurfaceController
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public LoginController(IUserService userService)
        {
            this.userService = userService;
            this.userService.MemberService = Services.MemberService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        public LoginController() : this(new UserService())
        {
        }

        /// <summary>
        /// Renders the login.
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderLogin()
        {
            LoginViewModel viewModel = new LoginViewModel();

            if (string.IsNullOrEmpty(HttpContext.Request["ReturnUrl"]))
            {
                viewModel.ReturnUrl = "/";
            }
            else
            {
                viewModel.ReturnUrl = HttpContext.Request["ReturnUrl"];
            }

            return PartialView("Login", viewModel);
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
            if (!ModelState.IsValid)
            {
                return PartialView("Login", viewModel);
            }

            if (userService.IsUserLoggedIn())
            {
                return Redirect("/");
            }

            try
            {
                bool result = userService.Login(viewModel.EmailAddress, viewModel.Password);

                if (result)
                {
                    IMember member = userService.GetUser(viewModel.EmailAddress);

                    userService.UpdateLoginStatus( member);

                    return new RedirectResult(viewModel.ReturnUrl);
                }

                ModelState.AddModelError("LoginForm.", "Invalid details");
                return PartialView("Login", viewModel);
            }
            catch (Exception ex)
            {
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
            if (userService.IsUserLoggedIn())
            {
                userService.Logout();
            }

            return Redirect("/");
        }
    }
}
