namespace Spectrum.Application.Customer.Providers
{
    using Model.Customer;
    using Repositories;

    /// <summary>
    /// The CustomerProvider class.
    /// </summary>
    internal class CustomerProvider : ICustomerProvider
    {
        /// <summary>
        /// The customer repository.
        /// </summary>
        private readonly ICustomerRepository customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProvider" /> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        internal CustomerProvider(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProvider"/> class.
        /// </summary>
        internal CustomerProvider()
            : this(new CustomerRepository())
        {
        }

        /// <summary>
        /// Emails the address updated.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailAddressUpdated(UpdateEmailAddressModel model)
        {
            customerRepository.EmailAddressUpdated(model);
        }

        /// <summary>
        /// Names the updated.
        /// </summary>
        /// <param name="model">The model.</param>
        public void NameUpdated(UpdateNameModel model)
        {
            customerRepository.NameUpdated(model);
        }
    }
}
