namespace Spectrum.Content.Appointments
{
    public static class AppointmentConstants
    {
        /// <summary>
        /// The appointment table name.
        /// </summary>
        public const string AppointmentTableName = "spectrumAppointment";

        /// <summary>
        /// The appointment attendee table name.
        /// </summary>
        public const string AppointmentAttendeeTableName = "spectrumAppointmentAttendee";

        /// <summary>
        /// The appointment attendee join statement.
        /// </summary>
        public const string AppointmentAttendeeJoinStatement = AppointmentTableName + ".Id = " + AppointmentAttendeeTableName + ".AppointmentId";

        /// <summary>
        /// The appointment status table name.
        /// </summary>
        public const string AppointmentStatusTableName = "spectrumAppointmentStatus";

        /// <summary>
        /// The ical appointment table name
        /// </summary>
        public const string IcalAppointmentTableName = "spectrumIcalAppointment";

        /// <summary>
        /// The last appointment identifier cookie.
        /// </summary>
        public const string LastAppointmentIdCookie = "lastAppointmentId";
    }
}
