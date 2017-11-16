namespace Spectrum.Content.Payments.Controllers
{
    using Content.Services;
    using Factories;
    using Managers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class PaymentController : BaseController
    {
        /// <summary>
        /// The payment manager.
        /// </summary>
        private readonly IPaymentManager paymentManager;

        /// <summary>
        /// The rules engine service.
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// The payment provider factory.
        /// </summary>
        private readonly IPaymentProviderFactory paymentProviderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.PaymentController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentManager">The payment manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <param name="paymentProviderFactory">The payment provider factory.</param>
        /// <inheritdoc />
        public PaymentController(
            ILoggingService loggingService,
            IPaymentManager paymentManager,
            IRulesEngineService rulesEngineService, 
            IPaymentProviderFactory paymentProviderFactory)
            : base(loggingService)
        {
            this.paymentManager = paymentManager;
            this.rulesEngineService = rulesEngineService;
            this.paymentProviderFactory = paymentProviderFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.PaymentController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentManager">The payment manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public PaymentController(
            UmbracoContext umbracoContext,
            ILoggingService loggingService,
            IPaymentManager paymentManager,
            IRulesEngineService rulesEngineService)
            : base(loggingService)
        {
            this.paymentManager = paymentManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.PaymentController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentManager">The payment manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public PaymentController(
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ILoggingService loggingService,
            IPaymentManager paymentManager,
            IRulesEngineService rulesEngineService)
            : base(loggingService)
        {
            this.paymentManager = paymentManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Gets the payment context.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetPaymentContext()
        {
            //// TODO :-move to a translator at some point?
            
            PaymentContextViewModel viewModel = new PaymentContextViewModel
            {
                AuthToken = paymentManager.GetAuthToken(UmbracoContext),
                Environment = paymentManager.GetEnvironment(UmbracoContext),
                NodeId = CurrentPage.Id.ToString(),
                MakePaymentUrl = "/umbraco/Surface/Payment/MakePayment",
                AutoAllocate = Request.QueryString[PaymentsQueryStringConstants.AutoAllocate],
                AppointmentId = Request.QueryString[PaymentsQueryStringConstants.AppointmentId],
                InvoiceId = Request.QueryString[PaymentsQueryStringConstants.InvoiceId],
                EmailAddress = Request.QueryString[PaymentsQueryStringConstants.EmailAddress],
                PaymentAmount = Request.QueryString[PaymentsQueryStringConstants.PaymenyAmount]
            };

            string jsonString = JsonConvert.SerializeObject(viewModel,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()

                });

            return Content(jsonString);
        }

        /// <summary>
        /// Gets the payment page.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetPaymentPage()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                ///// the factory will return different payment pages
                //// currently only braintree payments supported.
                string partialView = paymentProviderFactory.GetPaymentPartialView(UmbracoContext);

                return PartialView(partialView, new MakePaymentViewModel());
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Current Page Id Not Set</exception>
        [HttpPost]
        public JsonResult MakePayment(MakePaymentViewModel viewModel)
        {
           try
           {
               LoggingService.Info(GetType());

                if (string.IsNullOrEmpty(viewModel.CurrentPageNodeId))
                {
                    throw new ApplicationException("Current Page Id Not Set");
                }

                IPublishedContent publishedContent = GetContentById(viewModel.CurrentPageNodeId);

                string url = paymentManager.MakePayment(
                    UmbracoContext, 
                    publishedContent, 
                    viewModel,
                    Members.CurrentUserName);

                return Json(url);
            }
            catch (Exception e)
            {
                LoggingService.Error(GetType(), "Payment Error", e);
                return Json("/Error");
            }
        }
    }
}
