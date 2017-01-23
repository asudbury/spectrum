namespace Spectrum.Application.Registration.Repositories
{
    using Core.Services;
    using Model.Registration;
    using NPoco;

    internal class RegistrationRepository : IRegistrationRepository
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationRepository"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        public RegistrationRepository(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        /// <summary>
        /// The user has been registered.
        /// Here we need to create the user in the database.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserRegistered(RegisterModel model)
        {
            using (IDatabase db = new Database(databaseService.RegistrationConnectionString))
            {
                db.Insert(model);
            }
        }
    }
}
