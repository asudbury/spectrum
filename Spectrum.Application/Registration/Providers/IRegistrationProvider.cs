namespace Spectrum.Application.Registration.Providers
{
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
    }
}
