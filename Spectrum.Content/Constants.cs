namespace Spectrum.Content
{
    public static class Constants
    {
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
            /// The ical appointment table name.
            /// </summary>
            public const string IcalAppointmentTableName = "spectrumIcalAppointment";

            /// <summary>
            /// The quote table name.
            /// </summary>
            public const string QuoteTableName = "spectrumQuote";

            /// <summary>
            /// The invoice table name.
            /// </summary>
            public const string InvoiceTableName = "spectrumInvoice";

            /// <summary>
            /// The transactions table name.
            /// </summary>
            public const string TransactionsTableName = "spectrumTransaction";

            /// <summary>
            /// The address table name.
            /// </summary>
            public const string AddressTableName = "spectrumAddress";

            /// <summary>
            /// The client table name.
            /// </summary>
            public const string ClientTableName = "spectrumClient";
        }

        public static class Rules
        {
            /// <summary>
            /// The is customer quotes enabled.
            /// </summary>
            public const string IsCustomerQuotesEnabled = "IsCustomerQuotesEnabled";
            
            /// <summary>
            /// The is customer invoices enabled
            /// </summary>
            public const string IsCustomerInvoicesEnabled = "IsCustomerInvoicesEnabled";

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

        public static class PaymentProviders
        {
            /// <summary>
            /// Braintree.
            /// </summary>
            public const string Braintree = "Braintree";

            /// <summary>
            /// PayPal.
            /// </summary>
            public const string PayPal = "PayPal";
        }

        public static class Nodes
        {
            /// <summary>
            /// The settings node name.
            /// </summary>
            public const string SettingsNodeName = "SettingsNode";

            /// <summary>
            /// The customer node name.
            /// </summary>
            public const string CustomerNodeName = "CustomerNode";

            /// <summary>
            /// The menu node name.
            /// </summary>
            public const string MenuNodeName = "MenuNode";

            /// <summary>
            /// The payments node name.
            /// </summary>
            public const string PaymentsNodeName = "PaymentsNode";
            
            /// <summary>
            /// The appointments node name.
            /// </summary>
            public const string AppointmentsNodeName = "AppointmentsNode";

            /// <summary>
            /// The invoices node name.
            /// </summary>
            public const string InvoicesNodeName = "InvoicesNode";

            /// <summary>
            /// The quotes node name.
            /// </summary>
            public const string QuotesNodeName = "QuotesNode";

            /// <summary>
            /// The mail node name.
            /// </summary>
            public const string MailNodeName = "MailNode";

            /// <summary>
            /// The cards node name.
            /// </summary>
            public const string CardsNodeName = "CardsNode";
        }
    }
}
