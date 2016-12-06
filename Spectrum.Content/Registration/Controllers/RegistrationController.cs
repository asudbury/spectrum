
namespace Spectrum.Content.Registration.Controllers
{
    using Services;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web.Mvc;
    using ViewModels;

    public class RegistrationController : SurfaceController
    {
        private readonly IUserService userService;

        public RegistrationController(IUserService userService)
        {
            this.userService = userService;
            this.userService.MemberService = Services.MemberService;
        }

        public RegistrationController() : this(new UserService())
        {
        }
        public ActionResult RenderRegister()
        {
            return PartialView("Register", new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleRegister(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Register", viewModel);
            }

            IMember member = userService.CreateUser(
                viewModel.Name, 
                viewModel.Password, 
                viewModel.EmailAddress, 
                "MemberType");

            if (member == null)
            {
                ModelState.AddModelError("", "Member already exists");
                return CurrentUmbracoPage();
            }
            
            string guid = userService.GetUserGuid(member);

            //Send out verification email, with GUID in it
            ////EmailHelper email = new EmailHelper();
            ////email.SendVerifyEmail(model.EmailAddress, tempGUID.ToString());

            return PartialView("Register", new RegisterViewModel());
        }

        public JsonResult CheckEmailIsUsed(string emailAddress)
        {
            IMember member = userService.GetUser(emailAddress);

            if (member != null)
            {
                return Json($"The email address '{emailAddress}' is already in use.", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
