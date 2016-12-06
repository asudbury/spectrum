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
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
            this.userService.MemberService = Services.MemberService;
        }

        public LoginController() : this(new UserService())
        {
        }

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
