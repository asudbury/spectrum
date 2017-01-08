

namespace Spectrum.Application.Registration.Providers
{
    using Model;
    using Model.Registration;

    /// <summary>
    /// The IRegistrationProvider interface.
    /// </summary>
    public interface IRegistrationProvider
    {
        /// <summary>
        /// The user has registered.
        /// </summary>
        /// <param name="model">The model.</param>
        void UserRegistered(RegisterModel model);

        /// <summary>
        /// The user has been verified.
        /// </summary>
        /// <param name="model">The model.</param>
        void UserVerified(NotificationModel model);
    }
}
