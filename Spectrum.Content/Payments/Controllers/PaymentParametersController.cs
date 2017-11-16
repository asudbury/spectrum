namespace Spectrum.Content.Payments.Controllers
{
    using Content.Services;
    using Managers;
    using System.Web.Mvc;
    using Umbraco.Web;

    public class PaymentParametersController : BaseController
    {
        /// <summary>
        /// The payment manager.
        /// </summary>
        private readonly IPaymentManager paymentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentManager">The payment manager.</param>
        /// <inheritdoc />
        public PaymentParametersController(
            ILoggingService loggingService,
            IPaymentManager paymentManager) 
            : base(loggingService)
        {
            this.paymentManager = paymentManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="paymentManager">The payment manager.</param>
        /// <inheritdoc />
        public PaymentParametersController(
            ILoggingService loggingService, 
            UmbracoContext umbracoContext,
            IPaymentManager paymentManager) 
            : base(loggingService, umbracoContext)
        {
            this.paymentManager = paymentManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="paymentManager">The payment manager.</param>
        /// <inheritdoc />
        public PaymentParametersController(
            ILoggingService loggingService, 
            UmbracoContext umbracoContext, 
            UmbracoHelper umbracoHelper,
            IPaymentManager paymentManager) : 
            base(loggingService, umbracoContext, umbracoHelper)
        {
            this.paymentManager = paymentManager;
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAuthToken()
        {
            return Content(paymentManager.GetAuthToken(UmbracoContext));
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetEnvironment()
        {
            return Content(paymentManager.GetEnvironment(UmbracoContext));
        }

        /// <summary>
        /// Gets the automatic allocate.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAutoAllocate()
        {
            string autoallocate = Request.QueryString[PaymentsQueryStringConstants.AutoAllocate];
            return Content(autoallocate);
        }

        /// <summary>
        /// Gets the appointment identifier.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAppointmentId()
        {
            string appointmentId = Request.QueryString[PaymentsQueryStringConstants.AppointmentId];
            return Content(appointmentId);
        }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetEmailAddress()
        {
            string emailAddress = Request.QueryString[PaymentsQueryStringConstants.EmailAddress];
            return Content(emailAddress);
        }

        /// <summary>
        /// Gets the payment amount.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetPaymentAmount()
        {
            string paymentAmount = Request.QueryString[PaymentsQueryStringConstants.PaymenyAmount];
            return Content(paymentAmount);
        }
    }
}
