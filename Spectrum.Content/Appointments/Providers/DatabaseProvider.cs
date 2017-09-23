namespace Spectrum.Content.Appointments.Providers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core;
    using Umbraco.Core.Persistence;

    public class DatabaseProvider : IDatabaseProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The appointmentId</returns>
        public int InsertAppointment(AppointmentModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            object appointmentId = context.Database.Insert(model);

            int id = Convert.ToInt32(appointmentId);

            foreach (AppointmentAttendeeModel appointmentAttendeeModel in model.Attendees)
            {
                appointmentAttendeeModel.AppointmentId = id;
                InsertAppointmentAttendee(appointmentAttendeeModel);
            }

            return id;
        }

        /// <inheritdoc />
        /// <summary>
        /// Inters the appointment attendee.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertAppointmentAttendee(AppointmentAttendeeModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            context.Database.Insert(model);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the appointments.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        public IEnumerable<AppointmentModel> GetAppointments(
            DateTime dateRangeStart, 
            DateTime dateRangeEnd)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            int deleted = (int)AppointmentStatus.Deleted;

            /*Sql sql = new Sql()
                .Select("*")
                .From(AppointmentConstants.AppointmentTableName)
                .Where("Status != " + deleted + " and StartTime >= '" + dateRangeStart + "' and EndTime <= '" + dateRangeEnd + "'")
                .OrderBy("StartTime desc");*/

            Sql sql = new Sql()
                .Select("*")
                .From(AppointmentConstants.AppointmentTableName)
                .Where("Status != " + deleted)
                .OrderBy("StartTime desc");

            return context.Database.Fetch<AppointmentModel>(sql);
        }

        /// <summary>
        /// Gets the appointment.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public AppointmentModel GetAppointment(int appointmentId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            AppointmentModel model = context.Database.SingleOrDefault<AppointmentModel>(appointmentId);

            model.Attendees = GetAppointmentAttendees(appointmentId);

            return model;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the appointment attendees.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        public List<AppointmentAttendeeModel> GetAppointmentAttendees(int appointmentId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(AppointmentConstants.AppointmentAttendeeTableName)
                .Where("AppointmentId = " + appointmentId);

            return context.Database.Fetch<AppointmentAttendeeModel>(sql);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UpdateAppointment(AppointmentModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            context.Database.Update(model);
        }

        /// <inheritdoc />
        /// <summary>
        /// Insertis the cal appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertIcalAppointment(ICalAppointmentModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            context.Database.Insert(model);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the icalAppointment.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        public ICalAppointmentModel GetIcalAppointment(int appointmentId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(AppointmentConstants.IcalAppointmentTableName)
                .Where("AppointmentId = " + appointmentId);

            return context.Database.Fetch<ICalAppointmentModel>(sql).FirstOrDefault();
        }
    }
}
