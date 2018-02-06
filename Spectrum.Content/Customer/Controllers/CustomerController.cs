namespace Spectrum.Content.Customer.Controllers
{
    using Content.Services;
    using ContentModels;
    using Managers;
    using System.Web.Mvc;

    public class CustomerController : BaseController
    {
        /// <summary>
        /// Gets the customer manager.
        /// </summary>
        public ICustomerManager CustomerManager { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="customerManager">The customer manager.</param>
        /// <inheritdoc />
        public CustomerController(
            ILoggingService loggingService,
            ICustomerManager customerManager) 
            : base(loggingService)
        {
            CustomerManager = customerManager;
        }

        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        /// <param name="wsqdfff">The encrypted customer id.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult CustomerName(string wsqdfff)
        {
            CustomerModel model = CustomerManager.GetCustomerModel(
                UmbracoContext,
                wsqdfff);

            if (model != null)
            {
                return Content(model.CustomerName);
            }

            return Content(string.Empty);
        }
    }
}
