namespace Spectrum.Content.Appointments.Providers
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IDatabaseProvider
    {
        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The appointmentId</returns>
        string InsertAppointment(AppointmentModel model);

        /// <summary>
        /// Gets the appointments.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        IEnumerable<AppointmentModel> GetAppointments(
            DateTime dateRangeStart,
            DateTime dateRangeEnd);

        /// <summary>
        /// Gets the appointment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        AppointmentModel GetAppointment(int id);

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        void UpdateAppointment(AppointmentModel model);
    }
}
