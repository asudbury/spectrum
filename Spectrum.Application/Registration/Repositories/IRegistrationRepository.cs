namespace Spectrum.Application.Registration.Repositories
{
    using Model.Registration;

    public interface IRegistrationRepository
    {
        /// <summary>
        /// The user has registered.
        /// </summary>
        /// <param name="model">The model.</param>
        void UserRegistered(RegisterModel model);
    }
}
