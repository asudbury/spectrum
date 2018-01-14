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
        public int CreateAppointment(AppointmentModel model)
        {
            //// catch client id not set - might occur if code is regressed!
            if (model.ClientId == 0)
            {
                throw new ApplicationException("Create Appointment - Client Id not set");
            }

            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            object appointmentId = context.Database.Insert(model);

            int id = Convert.ToInt32(appointmentId);

            return id;
        }

        /// <summary>
        /// Gets the client appointments.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public IEnumerable<ClientAppointmentModel> GetClientAppointments(
            DateTime dateRangeStart, 
            DateTime dateRangeEnd, 
            int customerId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            int deleted = (int)AppointmentStatus.Deleted;

            Sql sql = new Sql()
                .Select("sc.Name as ClientName, sa.*")
                .From(Content.Constants.Database.AppointmentTableName + " sa")
                .InnerJoin(Content.Constants.Database.ClientTableName + " sc")
                .On("sa.ClientId = sc.Id")
                .Where("Status != " + deleted + " and sa.CustomerId=" + customerId)
                .OrderByDescending("StartTime");

            return context.Database.Fetch<ClientAppointmentModel>(sql);
        }
        
        /// <summary>
        /// Gets the client appointment.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public ClientAppointmentModel GetClientAppointment(
            int appointmentId, 
            int customerId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("sc.Name as ClientName, sa.*")
                .From(Content.Constants.Database.AppointmentTableName + " sa")
                .InnerJoin(Content.Constants.Database.ClientTableName + " sc")
                .On("sa.ClientId = sc.Id")
                .Where("sa.Id = " + appointmentId + " and sa.CustomerId= " + customerId);
            
            return context.Database.FirstOrDefault<ClientAppointmentModel>(sql);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UpdateAppointment(AppointmentModel model)
        {
            //// catch client id not set - might occur if code is regressed!
            if (model.ClientId == 0)
            {
                throw new ApplicationException("Update Appointment - Client Id not set");
            }

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
                .From(Content.Constants.Database.IcalAppointmentTableName)
                .Where("AppointmentId = " + appointmentId);

            return context.Database.Fetch<ICalAppointmentModel>(sql).FirstOrDefault();
        }
    }
}
