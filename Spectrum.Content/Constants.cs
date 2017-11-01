namespace Spectrum.Content
{
    public static class Constants
    {
        /// <summary>
        /// The default service provider identifier.
        /// </summary>
        public const int DefaultServiceProviderId = 1;

        public static class Database
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
            /// The appointment status table name.
            /// </summary>
            public const string AppointmentStatusTableName = "spectrumAppointmentStatus";

            /// <summary>
            /// The ical appointment table name
            /// </summary>
            public const string IcalAppointmentTableName = "spectrumIcalAppointment";
         }

        public static class Rules
        {
            /// <summary>
            /// The is customer appointments enabled.
            /// </summary>
            public const string IsCustomerAppointmentsEnabled = "IsCustomerAppointmentsEnabled";

            /// <summary>
            /// The is customer payments enabled.
            /// </summary>
            public const string IsCustomerPaymentsEnabled = "IsCustomerPaymentsEnabled";

            /// <summary>
            /// The is customer dashboard enabled.
            /// </summary>
            public const string IsCustomerDashboardEnabled = "IsCustomerDashboardEnabled";

            /// <summary>
            /// The is customer google calendar enabled.
            /// </summary>
            public const string IsCustomerGoogleCalendarEnabled = "IsCustomerGoogleCalendarEnabled";
        }
    }
}
