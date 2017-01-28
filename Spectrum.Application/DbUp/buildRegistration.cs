namespace Spectrum.Application.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection;
    using DbUp;
    using System.IO;
    using Core.Services;
    using System.Configuration;

    public class RegistrationDb
    {
        public RegistrationDb()
        {
        }

        public void Upgrade()
        {
            var databaseService = new DatabaseService();
            var connectionString = ConfigurationManager.ConnectionStrings[databaseService.RegistrationConnectionString].ToString();

            EnsureDatabase.For.SqlDatabase(connectionString);

            var location = ConfigurationManager.AppSettings["ScriptLocation"];

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(location)
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            //Console.WriteLine(result.Error);
            //Console.ReadLine();
        }
    }
}
