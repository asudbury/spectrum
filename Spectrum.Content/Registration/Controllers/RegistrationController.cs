namespace Spectrum.Content.Registration.Controllers
{
    using Model;
    using Providers;
    using System.Web.Mvc;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;
    using ViewModels;

    /// <summary>
    /// The Registration Controller.
    /// </summary>
    /// <seealso cref="Umbraco.Web.Mvc.SurfaceController" />
    public class RegistrationController : SurfaceController
    {
        /// <summary>
        /// The registration provider.
        /// </summary>
        private readonly IRegistrationProvider registrationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="registrationProvider">The registration provider.</param>
        public RegistrationController(
            UmbracoContext context,
            IRegistrationProvider registrationProvider)
            : base(context)
        {
            this.registrationProvider = registrationProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        /// <param name="registrationProvider">The registration provider.</param>
        public RegistrationController(IRegistrationProvider registrationProvider)
        {
            this.registrationProvider = registrationProvider;
            this.registrationProvider.MemberService = Services.MemberService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        public RegistrationController() : this(new RegistrationProvider())
        {
        }

        /// <summary>
        /// Renders the register.
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult RenderRegister()
        {
            return PartialView("Register", new RegisterViewModel());
        }

        /// <summary>
        /// Handles the register.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleRegister(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Register", viewModel);
            }

            RegisteredUser registeredUser = registrationProvider.RegisterUser(viewModel);

            if (registeredUser == null)
            {
                ModelState.AddModelError("", "Member already exists");
                return CurrentUmbracoPage();
            }
            
            return PartialView("Register", new RegisterViewModel());
        }

        /// <summary>
        /// Checks the email is in use.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>True or false.</returns>
        public JsonResult CheckEmailInUse(string emailAddress)
        {
            bool result = registrationProvider.CheckEmailInUse(emailAddress);

            if (!result)
            {
                return Json($"The email address '{emailAddress}' is already in use.", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
