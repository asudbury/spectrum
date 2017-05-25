namespace Spectrum.Content.Registration.Controllers
{
    using Models;
    using Providers;
    using Services;
    using Services.Mail;
    using System;
    using System.Web.Mvc;
    using Umbraco.Web;
    using ViewModels;

    /// <summary>
    /// The Registration Controller.
    /// </summary>
    public class RegistrationController : BaseController
    {
        /// <summary>
        /// The registration provider.
        /// </summary>
        private readonly IRegistrationProvider registrationProvider;

        /// <summary>
        /// The perplex mail service.
        /// </summary>
        private readonly IPerplexMailService perplexMailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="registrationProvider">The registration provider.</param>
        /// <param name="perplexMailService">The perplex mail service.</param>
        public RegistrationController(
            UmbracoContext context,
            ILoggingService loggingService,
            IRegistrationProvider registrationProvider,
            IPerplexMailService perplexMailService)
            : base(context,loggingService)
        {
            this.registrationProvider = registrationProvider;
            this.perplexMailService = perplexMailService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="registrationProvider">The registration provider.</param>
        /// <param name="perplexMailService">The perplex mail service.</param>
        public RegistrationController(
            ILoggingService loggingService,
            IRegistrationProvider registrationProvider,
            IPerplexMailService perplexMailService)
            :base(loggingService)
        {
            this.registrationProvider = registrationProvider;
            this.registrationProvider.MemberService = Services.MemberService;
            this.perplexMailService = perplexMailService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        public RegistrationController() : 
            this(new LoggingService(), new RegistrationProvider(), new PerplexMailService())
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
                    string message = viewModel.Name + " Member already exists";

                    LoggingService.Info(GetType(), message);
                    ModelState.AddModelError(string.Empty, message);
                    return PartialView("Register", viewModel);
                }

                //// now we want to send out the email!
                perplexMailService.SendEmail(1112, viewModel.EmailAddress);

                //// now navigate to the thankyou page

                string url = GetPageUrl(UserConstants.ThankYouPage);

                if (string.IsNullOrEmpty(url) == false)
                {
                    Response.Redirect(url);
                }

                return null;
            }
            catch (Exception e)
            {
                LoggingService.Error(GetType(), "Registration Error", e);
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

                    LoggingService.Info(GetType(), message);
                    ModelState.AddModelError("", message);
                    return CurrentUmbracoPage();
                }

                return PartialView("Verify", new VerifyUserViewModel());
            }
            catch (Exception e)
            {
                LoggingService.Error(GetType(), "Verification Error", e);
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
