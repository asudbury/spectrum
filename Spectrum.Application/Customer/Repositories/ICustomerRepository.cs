
namespace Spectrum.Application.Customer.Repositories
{
    using Model.Customer;

    public interface ICustomerRepository
    {
        /// <summary>
        /// Emails the address updated.
        /// </summary>
        /// <param name="model">The model.</param>
        void EmailAddressUpdated(UpdateEmailAddressModel model);

        /// <summary>
        /// Names the updated.
        /// </summary>
        /// <param name="model">The model.</param>
        void NameUpdated(UpdateNameModel model);
    }
}
