namespace Spectrum.Content.Payments.Controllers
{
    using Content.Models;
    using Content.Services;
    using Factories;
    using Managers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
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
                CustomerName = paymentManager.GetCustomerName(UmbracoContext),
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
        /// Gets the transactions page.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetTransactionsPage()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                string partialView = paymentProviderFactory
                                        .GetTransactionsPartialView(UmbracoContext, true);

                IEnumerable<TransactionViewModel> viewModels = paymentManager.GetTransactionsViewModel(UmbracoContext);

                return PartialView(partialView, viewModels);
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Gets the transaction page.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetTransactionPage(string opddewq)
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                string partialView = paymentProviderFactory
                    .GetTransactionPartialView(UmbracoContext, true);

                TransactionViewModel viewModel = paymentManager.GetTransactionViewModel(UmbracoContext, opddewq);

                return PartialView(partialView, viewModel);
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Gets the payment page.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetViewPaymentPage()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                ///// the factory will return different payment pages
                //// currently only braintree payments supported.
                string partialView = paymentProviderFactory.GetTransactionPartialView(UmbracoContext, true);

                return PartialView(partialView, new MakePaymentViewModel());
            }

            return default(PartialViewResult);
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
        /// Gets the boot grid transactions.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBootGridTransactions(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems)
        {
            if (rulesEngineService.IsCustomerPaymentsEnabled(UmbracoContext))
            {
                BootGridViewModel<TransactionViewModel> bootGridViewModel = paymentManager.GetBootGridTransactions(
                    current,
                    rowCount,
                    searchPhrase,
                    sortItems,
                    UmbracoContext);

                string jsonString = JsonConvert.SerializeObject(
                    bootGridViewModel,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()

                    });

                return Content(jsonString, "application/json");
            }

            return Content(string.Empty, "application/json");
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
