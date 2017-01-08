namespace Spectrum.Application.Customer.Repositories
{
    using Model.Customer;

    internal class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Emails the address updated.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailAddressUpdated(UpdateEmailAddressModel model)
        {
        }

        /// <summary>
        /// Names the updated.
        /// </summary>
        /// <param name="model">The model.</param>
        public void NameUpdated(UpdateNameModel model)
        {
        }
    }
}
