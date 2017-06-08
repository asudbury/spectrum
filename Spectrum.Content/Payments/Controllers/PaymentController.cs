
namespace Spectrum.Content.Payments.Controllers
{
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
        public ActionResult HandlePayment(PaymentViewModel viewModel)
        {
           try
            {
                LoggingService.Info(GetType(), "Entering HandlePayment");

                IPublishedContent currentPage = Umbraco.TypedContent(viewModel.CurrentPageNodeId);
                
                BraintreeModel model = new BraintreeModel(currentPage);

                LoggingService.Info(GetType(), "HandlePayment MakePayment");

                bool paymentMade = paymentProvider.MakePayment(model, viewModel);

                if (paymentMade)
                {
                    LoggingService.Info(GetType(), "Payment Succesful");

                    if (model.EmailTemplateNodeId.HasValue)
                    {
                        LoggingService.Info(GetType(), "Sending Email");

                        perplexMailService.SendEmail(model.EmailTemplateNodeId.Value, viewModel.EmailAddress);
                    }

                    return Content(GetPageUrl(model.NextPageNodeId));
                }

                LoggingService.Info(GetType(), "Payment Failed");

                return Content(GetPageUrl(model.ErrorPageNodeId));
            }
            catch (Exception e)
            {
                LoggingService.Error(GetType(), "Payment Error", e);
                throw;
            }
        }
    }
}
