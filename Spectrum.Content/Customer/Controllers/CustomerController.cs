namespace Spectrum.Content.Customer.Controllers
{
    using Content.Services;
    using ContentModels;
    using Providers;
    using System.Web.Mvc;

    public class CustomerController : BaseController
    {
        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <inheritdoc />
        public CustomerController(
            ILoggingService loggingService,
            ICustomerProvider customerProvider) 
            : base(loggingService)
        {
            this.customerProvider = customerProvider;
        }

        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult CustomerName()
        {
            CustomerModel model = customerProvider.GetCustomerModel(UmbracoContext);

            if (model != null)
            {
                return Content(model.CustomerName);
            }

            return Content(string.Empty);
        }
    }
}
