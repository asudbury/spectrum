namespace Spectrum.Content.Payments.Controllers
{
    using Application.Services;
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
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Payments.Controllers.PaymentController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="paymentManager">The payment manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <param name="paymentProviderFactory">The payment provider factory.</param>
        /// <param name="encryptionService">The encryption service.</param>
        /// <inheritdoc />
        public PaymentController(
            ILoggingService loggingService,
            IPaymentManager paymentManager,
            IRulesEngineService rulesEngineService, 
            IPaymentProviderFactory paymentProviderFactory,
            IEncryptionService encryptionService)
            : base(loggingService)
        {
            this.paymentManager = paymentManager;
            this.rulesEngineService = rulesEngineService;
            this.paymentProviderFactory = paymentProviderFactory;
            this.encryptionService = encryptionService;
        }

        /// <summary>
        /// Gets the payment context.
        /// </summary>
        /// <param name="wsqdfff">The encrypted customer id.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetPaymentContext(string wsqdfff)
        {
            //// TODO :-move to a translator at some point?
            
            PaymentContextViewModel viewModel = new PaymentContextViewModel
            {
                AuthToken = paymentManager.GetAuthToken(UmbracoContext, wsqdfff),
                Environment = paymentManager.GetEnvironment(UmbracoContext, wsqdfff),
                NodeId = CurrentPage.Id.ToString(),
                MakePaymentUrl = "/umbraco/Surface/Payment/MakePayment",
                InvoiceId = Request.QueryString[Constants.QueryString.InvoiceId],
                ClientId = Request.QueryString[Constants.QueryString.ClientId],
                CustomerId = wsqdfff,
                PaymentAmount = encryptionService.DecryptString(Request.QueryString[Constants.QueryString.PaymenyAmount])
            };

            string jsonString = JsonConvert.SerializeObject(viewModel, new JsonSerializerSettings
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

            if (rulesEngineService.IsCustomerPaymentsEnabled())
            {
                string partialView = paymentProviderFactory.GetTransactionsPartialView(UmbracoContext, true);

                IEnumerable<TransactionViewModel> viewModels = paymentManager.GetTransactionsViewModel(UmbracoContext);

                return PartialView(partialView, viewModels);
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Gets the transaction page.
        /// </summary>
        /// <param name="fkdssre">The clietn id.</param>
        /// <param name="opddewq">The opddewq.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetTransactionPage(
            string fkdssre,
            string opddewq)
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerPaymentsEnabled())
            {
                string partialView = paymentProviderFactory.GetTransactionPartialView(UmbracoContext, true);

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

            if (rulesEngineService.IsCustomerPaymentsEnabled())
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
        /// <param name="fkdssre">The encrypted client id.</param>
        /// <param name="wsqdfff">The eccrypted customer id.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetPaymentPage(
            string fkdssre,
            string wsqdfff)
        {
            LoggingService.Info(GetType());

            int? customerId = encryptionService.DecryptNumber(wsqdfff);

            ///// the factory will return different payment pages
            //// currently only braintree payments supported.
            string partialView = paymentProviderFactory.GetPaymentPartialView(
                                                            UmbracoContext,
                                                            customerId);

            MakePaymentViewModel viewModel = new MakePaymentViewModel
            {
                ClientId = fkdssre,
                CustomerId = wsqdfff
            };

            return PartialView(partialView, viewModel);
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
            if (rulesEngineService.IsCustomerPaymentsEnabled())
            {
                string jsonString = paymentManager.GetBootGridTransactions(
                    current,
                    rowCount,
                    searchPhrase,
                    sortItems,
                    UmbracoContext);

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
