namespace Spectrum.Content.Payments.Controllers
{
    using Content.Services;
    using ContentModels;
    using Mail.Models;
    using Mail.Providers;
    using Providers;
    using System;
    using System.Web.Mvc;
    using ViewModels;

    public class PaymentController : BaseController
    {
        /// <summary>
        /// The payment provider.
        /// </summary>
        private readonly IPaymentProvider paymentProvider;
        
        /// <summary>
        /// The mail provider.
        /// </summary>
        private readonly IMailProvider mailProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="mailProvider">The mail provider.</param>
        public PaymentController(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            IMailProvider mailProvider) 
            : base(loggingService)
        {
            this.paymentProvider = paymentProvider;
            this.mailProvider = mailProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        public PaymentController()
            : this(new LoggingService(), 
                   new PaymentProvider(), 
                   new MailProvider())
        {
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAuthToken()
        {
            BraintreeModel model = paymentProvider.GetBraintreeModel(UmbracoContext);

            string token = paymentProvider.GetAuthToken(model);

            return Content(token);
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetEnvironment()
        {
            BraintreeModel model = paymentProvider.GetBraintreeModel(UmbracoContext);

            return Content(model.Environment);
        }

        /// <summary>
        /// Handles the payment.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        public JsonResult HandlePayment(PaymentViewModel viewModel)
        {
           try
            {
                LoggingService.Info(GetType(), "Entering HandlePayment");

                if (string.IsNullOrEmpty(viewModel.CurrentPageNodeId))
                {
                    throw new ApplicationException("Current Page Id Not Set");
                }

                PageModel pageModel = new PageModel(GetContentById(viewModel.CurrentPageNodeId));

                if (string.IsNullOrEmpty(pageModel.NextPageUrl))
                {
                    throw new ApplicationException("Next Page Url Not Set");
                }

                if (string.IsNullOrEmpty(pageModel.ErrorPageUrl))
                {
                    throw new ApplicationException("Error Page Url Not Set");
                }

                BraintreeModel model = paymentProvider.GetBraintreeModel(UmbracoContext);

                LoggingService.Info(GetType(), "HandlePayment MakePayment");

                bool paymentMade = paymentProvider.MakePayment(model, viewModel);

                if (paymentMade)
                {
                    LoggingService.Info(GetType(), "Payment Succesful");

                    if (pageModel.EmailTemplateNodeId.HasValue)
                    {
                        LoggingService.Info(GetType(), "Sending Email");

                        MailResponse mailResponse = mailProvider.SendEmail(
                                                        UmbracoContext, 
                                                        pageModel.EmailTemplateNodeId.Value, 
                                                        viewModel.EmailAddress);

                        //// TODO : we need to log the mail response!
                    }

                    return Json(pageModel.NextPageUrl);
                }

                LoggingService.Info(GetType(), "Payment Failed");

                return Json(pageModel.ErrorPageUrl);
            }
            catch (Exception e)
            {
                LoggingService.Error(GetType(), "Payment Error", e);
                return Json("/Error");
            }
        }
    }
}
