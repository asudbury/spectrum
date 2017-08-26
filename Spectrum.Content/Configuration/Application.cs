namespace Spectrum.Content.Configuration
{
    using Appointments;
    using Appointments.Models;
    using System;
    using Umbraco.Core;
    using Umbraco.Core.Persistence;

    /// <summary>
    /// 
    /// </summary>
    public static class Application
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
            DatabaseContext databaseContext = applicationContext.DatabaseContext;

            DatabaseSchemaHelper db = new DatabaseSchemaHelper(
                databaseContext.Database,
                applicationContext.ProfilingLogger.Logger,
                databaseContext.SqlSyntax);

            //// TODO : maybe only create tables if appointments are supported??
             
            if (!db.TableExist(AppointmentConstants.AppointmentStatusTableName))
            {
                //// create look up table for the appointment status

                db.CreateTable<AppointmentStatusModel>(false);

                foreach(AppointmentStatus status in Enum.GetValues(typeof(AppointmentStatus)))
                {
                    databaseContext.Database.Insert(new AppointmentStatusModel { Id = (int)status, Description = status.ToString() });
                }
            }

            if (!db.TableExist(AppointmentConstants.AppointmentTableName))
            {
                db.CreateTable<AppointmentModel>(false);
            }

            if (!db.TableExist(AppointmentConstants.AppointmentAttendeeTableName))
            {
                db.CreateTable<AppointmentAttendeeModel>(false);
            }

            if (!db.TableExist(AppointmentConstants.iCalAppointmentTableName))
            {
                db.CreateTable<ICalAppointmentModel>(false);
            }
        }
    }
}
