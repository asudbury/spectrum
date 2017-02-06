namespace Spectrum.Database.Registration.Repositories
{
    using System;
    using DbUp;
    using DbUp.Engine;
    using Core.Services;
    using System.Configuration;
    using Model.Correspondence;
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
        /// Initializes a new instance of the <see cref="RegistrationRepository"/> class.
        /// </summary>
        public RegistrationRepository()
            :this(new DatabaseService())
        {
        }

        /// <summary>
        /// The user has been registered.
        /// Here we need to create the user in the database.
        /// </summary>
        public void Bootstrap()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[databaseService.RegistrationConnectionString].ToString();

            EnsureDatabase.For.SqlDatabase(connectionString);

            string location = ConfigurationManager.AppSettings["ScriptLocation"];

            UpgradeEngine upgrader = DeployChanges
                                       .To
                                       .SqlDatabase(connectionString)
                                       .WithScriptsFromFileSystem(location)
                                       .LogToConsole()
                                       .Build();

            upgrader.PerformUpgrade();

            //// Bootstrap registration static data
            using (IDatabase db = new Database(databaseService.RegistrationConnectionString))
            {
                foreach (Event eventModel in Enum.GetValues(typeof(Event)))
                {
                    string eventDescription = eventModel.ToString();

                    EventTypeModel eventTypeModel = new EventTypeModel(eventModel, eventDescription);

                    //// Does this static data item exist.
                    if (db.IsNew(eventTypeModel))
                    {
                        db.Insert(eventTypeModel);
                    }
                }
            }
        }
    }
}
