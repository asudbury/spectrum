namespace Spectrum.Application.Customer.Providers
{
    using Model.Customer;

    /// <summary>
    /// The ICustomerProvider interface.
    /// </summary>
    public interface ICustomerProvider
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
