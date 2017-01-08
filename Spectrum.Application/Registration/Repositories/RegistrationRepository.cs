namespace Spectrum.Application.Registration.Repositories
{
    using Model;
    using Model.Registration;

    internal class RegistrationRepository : IRegistrationRepository
    {
        /// <summary>
        /// The user has been registered.
        /// Here we need to create the user in the database.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserRegistered(RegisterModel model)
        {
            //// TODO : here we need to create the user in the database
        }

        /// <summary>
        /// The user has been verified.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserVerified(NotificationModel model)
        {
            ///// TODO : here we need to verify the user - this will effectively activate the user.
        }
    }
}
