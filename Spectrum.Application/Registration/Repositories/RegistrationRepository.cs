namespace Spectrum.Application.Registration.Repositories
{
    using Model.Registration;
    using NPoco;

    internal class RegistrationRepository : IRegistrationRepository
    {
        /// <summary>
        /// The user has been registered.
        /// Here we need to create the user in the database.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserRegistered(RegisterModel model)
        {
            //// TODO : we want to read the connection string from web/app.config at some point
            IDatabase db = new Database("Spectrum");
            db.Insert(model);
        }
    }
}
