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
        int InsertAppointment(AppointmentModel model);
        
        /// <summary>
        /// Inserts the appointment attendee.
        /// </summary>
        /// <param name="model">The model.</param>
        void InsertAppointmentAttendee(AppointmentAttendeeModel model);

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
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        AppointmentModel GetAppointment(int appointmentId);

        /// <summary>
        /// Gets the appointment attendees.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        List<AppointmentAttendeeModel> GetAppointmentAttendees(int appointmentId);

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
