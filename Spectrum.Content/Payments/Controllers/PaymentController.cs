namespace Spectrum.Content.Payments.Controllers
{
    using Content.Services;
    using ContentModels;
    using Mail.Models;
    using Mail.Services;
    using Providers;
    using System;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class PaymentController : BaseController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The payment provider.
        /// </summary>
        private readonly IPaymentProvider paymentProvider;

        /// <summary>
        /// The mail service.
        /// </summary>
        private readonly IMailService mailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="mailService">The mail service.</param>
        public PaymentController(
            ILoggingService loggingService,
            ISettingsService settingsService,
            IPaymentProvider paymentProvider,
            IMailService mailService) 
            : base(loggingService)
        {
            this.settingsService = settingsService;
            this.paymentProvider = paymentProvider;
            this.mailService = mailService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="mailService">The mail service.</param>
        public PaymentController(
            UmbracoContext context, 
            ILoggingService loggingService,
            ISettingsService settingsService,
            IPaymentProvider paymentProvider,
            IMailService mailService) 
            : base(context, loggingService)
        {
            this.paymentProvider = paymentProvider;
            this.mailService = mailService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        public PaymentController()
            : this(new LoggingService(), 
                   new SettingsService(), 
                   new PaymentProvider(), 
                   new MailService())
        {
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAuthToken()
        {
            BraintreeModel model = paymentProvider.GetBraintreeModel(settingsService.GetPaymentsNode(UmbracoContext));

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
            BraintreeModel model = paymentProvider.GetBraintreeModel(settingsService.GetPaymentsNode(UmbracoContext));

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

                BraintreeModel model = paymentProvider.GetBraintreeModel(settingsService.GetPaymentsNode(UmbracoContext));

                LoggingService.Info(GetType(), "HandlePayment MakePayment");

                bool paymentMade = paymentProvider.MakePayment(model, viewModel);

                if (paymentMade)
                {
                    LoggingService.Info(GetType(), "Payment Succesful");

                    if (pageModel.EmailTemplateNodeId.HasValue)
                    {
                        LoggingService.Info(GetType(), "Sending Email");

                        IPublishedContent content = settingsService.GetMailTemplateById(
                                                        UmbracoContext, 
                                                        pageModel.EmailTemplateNodeId.Value);

                        if (content != null)
                        {
                            MailTemplateModel mailTemplateModel = new MailTemplateModel(content);

                            MailResponse mailResponse = mailService.SendEmail(viewModel.EmailAddress, mailTemplateModel);


                            //// TODO : we need to log the mail response!
                        }

                        else
                        {
                            LoggingService.Info(GetType(), "Cannot find email template id=" + pageModel.EmailTemplateNodeId);
                        }
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
