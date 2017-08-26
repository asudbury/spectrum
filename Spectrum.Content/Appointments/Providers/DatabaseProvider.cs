﻿namespace Spectrum.Content.Appointments.Providers
{
    using Models;
    using System;
    using System.Collections.Generic;
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

            return Convert.ToInt32(appointmentId);
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

        /// <inheritdoc />
        /// <summary>
        /// Gets the appointment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public AppointmentModel GetAppointment(int id)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            return context.Database.SingleOrDefault<AppointmentModel>(id);
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
        public void InsertiCalAppointment(ICalAppointmentModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            context.Database.Insert(model);
        }
    }
}
