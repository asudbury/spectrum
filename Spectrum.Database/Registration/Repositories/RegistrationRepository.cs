namespace Spectrum.Database.Registration.Repositories
{
    using System;
    using System.IO;
    using DbUp;
    using DbUp.Engine;
    using DbUp.SqlCe;
    using System.Data.SqlServerCe;
    using System.Data.SqlClient;
    using Core.Services;
    using System.Configuration;
    using Model.Correspondence;
    using NPoco;
    using NPoco.DatabaseTypes;

    internal class RegistrationRepository : IRegistrationRepository
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationRepository"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        public RegistrationRepository(IDatabaseService databaseService, ISettingsService settingsService)
        {
            this.databaseService = databaseService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationRepository"/> class.
        /// </summary>
        public RegistrationRepository()
            :this(new DatabaseService(), new SettingsService())
        {
        }

        /// <summary>
        /// The user has been registered.
        /// Here we need to create the user in the database.
        /// </summary>
        public void Bootstrap()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[databaseService.RegistrationConnectionString].ToString();

            //Database type defaults to SLQServer but can be modified to SQLCe in the config.
            if (settingsService.CreateSQLCeDatabase)
            {
                var cs = new SqlConnectionStringBuilder(connectionString);
                string dataSource = cs.DataSource;

                //Create the SQLCe database if it does not exist
                if (!File.Exists(dataSource))
                {
                    var en = new SqlCeEngine(connectionString);
                    en.CreateDatabase();
                }

                string location = ConfigurationManager.AppSettings["ScriptLocationSQLCe"];

                //Ensure all scripts have been run
                var upgrader = DeployChanges
                                .To
                                .SqlCeDatabase(connectionString)
                                .WithScriptsFromFileSystem(location)
                                .LogToConsole()
                                .Build();

                upgrader.PerformUpgrade();
            }
            else
            {
                //Defaulting to SQLServer
                EnsureDatabase.For.SqlDatabase(connectionString);

                string location = ConfigurationManager.AppSettings["ScriptLocationSQLServer"];

                UpgradeEngine upgrader = DeployChanges
                                           .To
                                           .SqlDatabase(connectionString)
                                           .WithScriptsFromFileSystem(location)
                                           .LogToConsole()
                                           .Build();

                upgrader.PerformUpgrade();
            }

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
