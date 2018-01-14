namespace Spectrum.Content.Appointments.Providers
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IDatabaseProvider
    {
        /// <summary>
        /// Creates the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The appointmentId</returns>
        int CreateAppointment(AppointmentModel model);
        
        /// <summary>
        /// Gets the client appointments.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        IEnumerable<ClientAppointmentModel> GetClientAppointments(
            DateTime dateRangeStart,
            DateTime dateRangeEnd,
            int customerId);

        /// <summary>
        /// Gets the client appointment.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        ClientAppointmentModel GetClientAppointment(
            int appointmentId,
            int customerId);

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        void UpdateAppointment(AppointmentModel model);

        /// <summary>
        /// Insertis the cal appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        void InsertIcalAppointment(ICalAppointmentModel model);

        /// <summary>
        /// Getis the cal appointment.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        ICalAppointmentModel GetIcalAppointment(int appointmentId);
    }
}
