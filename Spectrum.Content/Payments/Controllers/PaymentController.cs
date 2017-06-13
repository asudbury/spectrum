namespace Spectrum.Content.Payments.Controllers
{
    using Braintree;
    using Content.Services;
    using Content.Services.Mail;
    using ContentModels;
    using Providers;
    using System;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class PaymentController : BaseController
    {
        /// <summary>
        /// The payment provider.
        /// </summary>
        private readonly IPaymentProvider paymentProvider;

        /// <summary>
        /// The perplex mail service.
        /// </summary>
        private readonly IPerplexMailService perplexMailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="perplexMailService">The perplex mail service.</param>
        public PaymentController(
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            IPerplexMailService perplexMailService) 
            : base(loggingService)
        {
            this.paymentProvider = paymentProvider;
            this.perplexMailService = perplexMailService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentProvider">The payment provider.</param>
        /// <param name="perplexMailService">The perplex mail service.</param>
        public PaymentController(
            UmbracoContext context, 
            ILoggingService loggingService,
            IPaymentProvider paymentProvider,
            IPerplexMailService perplexMailService) 
            : base(context, loggingService)
        {
            this.paymentProvider = paymentProvider;
            this.perplexMailService = perplexMailService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        public PaymentController()
            : this(new LoggingService(), 
                   new PaymentProvider(), 
                   new PerplexMailService())
        {
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAuthToken()
        {
            BraintreeModel model = new BraintreeModel(CurrentPage);

            string token = paymentProvider.GetAuthToken(model);

            return Content(token);
        }

        /// <summary>
        /// Gets the node identifier.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetNodeId()
        {
            return Content(CurrentPage.Id.ToString());
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

                IPublishedContent currentPage = Umbraco.TypedContent(viewModel.CurrentPageNodeId);

                BraintreeModel model = new BraintreeModel(currentPage);

                if (string.IsNullOrEmpty(model.NextPageUrl))
                {
                    throw new ApplicationException("Next Page Url Not Set");
                }

                if (string.IsNullOrEmpty(model.ErrorPageUrl))
                {
                    throw new ApplicationException("Error Page Url Not Set");
                }

                LoggingService.Info(GetType(), "HandlePayment MakePayment");

                bool paymentMade = paymentProvider.MakePayment(model, viewModel);

                if (paymentMade)
                {
                    LoggingService.Info(GetType(), "Payment Succesful");

                    /*if (model.EmailTemplateNodeId.HasValue)
                    {
                        LoggingService.Info(GetType(), "Sending Email");

                        perplexMailService.SendEmail(model.EmailTemplateNodeId.Value, viewModel.EmailAddress);
                    }*/

                    return Json(model.NextPageUrl);
                }

                LoggingService.Info(GetType(), "Payment Failed");

                return Json(model.ErrorPageUrl);
            }
            catch (Exception e)
            {
                LoggingService.Error(GetType(), "Payment Error", e);
                return Json("/Error");
            }
        }

        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetTransactions()
        {
            IPublishedContent currentPage = Umbraco.TypedContent(CurrentPage.Id.ToString());

            BraintreeModel model = new BraintreeModel(currentPage);

            ResourceCollection<Transaction> transactions = paymentProvider.GetTransactions(model);

            return null;
        }
    }
}
