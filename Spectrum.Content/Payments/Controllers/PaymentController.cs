namespace Spectrum.Content.Payments.Controllers
{
    using Content.Services;
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

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.PaymentController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentManager">The payment manager.</param>
        public PaymentController(
            ILoggingService loggingService,
            IPaymentManager paymentManager)
            : base(loggingService)
        {
            this.paymentManager = paymentManager;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.PaymentController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentManager">The payment manager.</param>
        public PaymentController(
            UmbracoContext umbracoContext,
            ILoggingService loggingService,
            IPaymentManager paymentManager)
            : base(loggingService)
        {
            this.paymentManager = paymentManager;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.PaymentController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentManager">The payment manager.</param>
        public PaymentController(
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ILoggingService loggingService,
            IPaymentManager paymentManager)
            : base(loggingService)
        {
            this.paymentManager = paymentManager;
        }

        /// <summary>
        /// Gets the take payment view model.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetTakePaymentViewModel()
        {
            LoggingService.Info(GetType());

            string jsonString = JsonConvert.SerializeObject(
                paymentManager.GetTakePaymentViewModel(UmbracoContext, Request.QueryString),
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()

                });

            return Content(jsonString, "application/json");
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAuthToken()
        {
            LoggingService.Info(GetType());

            return Content(paymentManager.GetAuthToken(UmbracoContext));
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetEnvironment()
        {
            LoggingService.Info(GetType());

            return Content(paymentManager.GetEnvironment(UmbracoContext));
        }

        /// <summary>
        /// Gets the automatic allocate.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAutoAllocate()
        {
            LoggingService.Info(GetType());

            string autoallocate = Request.QueryString["autoAllocate"];

            return Content(autoallocate);
        }

        /// <summary>
        /// Gets the appointment identifier.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAppointmentId()
        {
            LoggingService.Info(GetType());

            string appointmentId = Request.QueryString[PaymentsQueryStringConstants.AppointmentId];

            return Content(appointmentId);
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
               LoggingService.Info(GetType());

                if (string.IsNullOrEmpty(viewModel.CurrentPageNodeId))
                {
                    throw new ApplicationException("Current Page Id Not Set");
                }

                IPublishedContent publishedContent = GetContentById(viewModel.CurrentPageNodeId);

                string url = paymentManager.HandlePayment(
                    UmbracoContext, 
                    publishedContent, 
                    viewModel);

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
