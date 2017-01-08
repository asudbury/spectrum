namespace Spectrum.Application.Customer.Controllers
{
    using Model.Customer;
    using Providers;

    public class CustomerController
    {
        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController" /> class.
        /// </summary>
        /// <param name="customerProvider">The customer provider.</param>
        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        public CustomerController()
            : this(new CustomerProvider())
        {
        }

        /// <summary>
        /// The user has updated their email address.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailAddressUpdated(UpdateEmailAddressModel model)
        {
            customerProvider.EmailAddressUpdated(model);
        }

        /// <summary>
        /// The user updated their name.
        /// </summary>
        /// <param name="model">The model.</param>
        public void NameUpdated(UpdateNameModel model)
        {
            customerProvider.NameUpdated(model);
        }
    }
}
