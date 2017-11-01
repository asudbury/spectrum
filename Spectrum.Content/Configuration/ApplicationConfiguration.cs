namespace Spectrum.Content.Configuration
{
    using Appointments.Models;
    using Extensions;
    using System;
    using Umbraco.Core;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Logging;

    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Setups this instance.
        /// </summary>
        /// <param name="umbracoApplication">The umbraco application.</param>
        /// <param name="applicationContext">The application context.</param>
        public static void Started(
            UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            LogHelper.Info(typeof(ApplicationConfiguration), "ApplicationStarted");

            DatabaseContext databaseContext = applicationContext.DatabaseContext;

            DatabaseSchemaHelper db = new DatabaseSchemaHelper(
                databaseContext.Database,
                applicationContext.ProfilingLogger.Logger,
                databaseContext.SqlSyntax);

            bool created = db.CreateTableIfNotExist<AppointmentStatusModel>(Content.Constants.Database.AppointmentStatusTableName);

            if (created)
            {
                foreach(AppointmentStatus status in Enum.GetValues(typeof(AppointmentStatus)))
                {
                    databaseContext.Database.Insert(new AppointmentStatusModel { Id = (int)status, Description = status.ToString() });
                }
            }

            db.CreateTableIfNotExist<AppointmentModel>(Content.Constants.Database.AppointmentTableName);
            db.CreateTableIfNotExist<AppointmentAttendeeModel>(Content.Constants.Database.AppointmentAttendeeTableName);
            db.CreateTableIfNotExist<ICalAppointmentModel>(Content.Constants.Database.IcalAppointmentTableName);
        }
    }
}
