
namespace Spectrum.Content.Payments.Controllers
{
    using Providers;
    using System.Web.Mvc;
    using System;
    using Content.Services;
    using ViewModels;
    using Umbraco.Web;

    public class PaymentController : BaseController
    {
        /// <summary>
        /// The payment provider.
        /// </summary>
        private readonly IPaymentProvider paymentProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        public PaymentController(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider) 
            : base(loggingService)
        {
            this.paymentProvider = paymentProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        public PaymentController(
            UmbracoContext context, 
            ILoggingService loggingService,
            IPaymentProvider paymentProvider) 
            : base(context, loggingService)
        {
            this.paymentProvider = paymentProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        public PaymentController()
            : this(new LoggingService(), new PaymentProvider())
        {
        }

        /// <summary>
        /// Renders the payment partial view.
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult RenderPayment()
        {
            return PartialView("Payment", new PaymentViewModel());
        }

        /// <summary>
        /// Handles the payment.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandlePayment(PaymentViewModel viewModel)
        {
           try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("Payment", viewModel);
                }

                ////RegisteredUser registeredUser = registrationProvider.RegisterUser(viewModel);

                /*if (registeredUser == null)
                {
                    string message = viewModel.Name + " Member already exists";

                    LoggingService.Info(GetType(), message);
                    ModelState.AddModelError(string.Empty, message);
                    return PartialView("Payment", viewModel);
                }*/

                //// now we want to send out the email!
                ////perplexMailService.SendEmail(1112, viewModel.EmailAddress);

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
                LoggingService.Error(GetType(), "Payment Error", e);
                throw;
            }
            
        }
        
    }
}
