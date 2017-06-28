using DbUp;
using DbUp.Engine;
using Spectrum.Core.Services;

namespace Spectrum.Application.DbUp
{
    using System;
    using System.Configuration;
    using Model.Correspondence;
    using NPoco;

    public class RegistrationDatabase
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationDatabase" /> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        public RegistrationDatabase(
            IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
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
