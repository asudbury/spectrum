namespace Spectrum.Content.Registration.Controllers
{
    using Models;
    using Providers;
    using Services;
    using System;
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
        /// The logging service.
        /// </summary>
        private readonly ILoggingService loggingService;

        /// <summary>
        /// The registration provider.
        /// </summary>
        private readonly IRegistrationProvider registrationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="registrationProvider">The registration provider.</param>
        public RegistrationController(
            UmbracoContext context,
            ILoggingService loggingService,
            IRegistrationProvider registrationProvider)
            : base(context)
        {
            this.loggingService = loggingService;
            this.registrationProvider = registrationProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="registrationProvider">The registration provider.</param>
        public RegistrationController(
            ILoggingService loggingService,
            IRegistrationProvider registrationProvider)
        {
            this.loggingService = loggingService;
            this.registrationProvider = registrationProvider;
            this.registrationProvider.MemberService = Services.MemberService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        public RegistrationController() : 
            this(new LoggingService(), new RegistrationProvider())
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("Register", viewModel);
                }

                RegisteredUser registeredUser = registrationProvider.RegisterUser(viewModel);

                if (registeredUser == null)
                {
                    string message = viewModel.Name + "Member already exists";

                    loggingService.Info(GetType(), message);
                    ModelState.AddModelError("", message);
                    return PartialView("Register", viewModel);
                }

                //// now we want to send out the email!

                //// now navigate to the thankyou page
                if (CurrentPage.GetProperty(UserConstants.ThankYouPage) != null)
                {
                    int nodeId = Convert.ToInt32(CurrentPage.GetProperty(UserConstants.ThankYouPage).DataValue);
                    string url = Umbraco.TypedContent(nodeId).Url;

                    //// not sure if we should do a redirect like this!
                    Response.Redirect(url);
                }

                return null;
            }
            catch (Exception e)
            {
                loggingService.Error(GetType(), "Registration Error", e);
                throw;
            }
        }

        /// <summary>
        /// Handles the verify user.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleVerifyUser(VerifyUserViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("Verify", viewModel);
                }

                bool result = registrationProvider.VerifyUser(viewModel);

                if (!result)
                {
                    string message = "Invalid verification";

                    loggingService.Info(GetType(), message);
                    ModelState.AddModelError("", message);
                    return CurrentUmbracoPage();
                }

                return PartialView("Verify", new VerifyUserViewModel());
            }
            catch (Exception e)
            {
                loggingService.Error(GetType(), "Verification Error", e);
                throw;
            }
        }

        /// <summary>
        /// Checks the email is in use.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>True or false.</returns>
        public JsonResult CheckEmailInUse(string emailAddress)
        {
            bool result = registrationProvider.CheckEmailInUse(emailAddress);

            if (result)
            {
                return Json($"The email address '{emailAddress}' is already in use.", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
