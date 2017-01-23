namespace Spectrum.Core.Services
{
    using System.Configuration;

    public class DatabaseService : IDatabaseService
    {
        /// <summary>
        /// The appointments key.
        /// </summary>
        private const string AppointmentsKey = "Spectrum-Appointments";

        /// <summary>
        /// The authentication key.
        /// </summary>
        private const string AuthenticationKey = "Spectrum-Authentication";

        /// <summary>
        /// The correspondence key.
        /// </summary>
        private const string CorrespondenceKey = "Spectrum-Correspondence";

        /// <summary>
        /// The customer key.
        /// </summary>
        private const string CustomerKey = "Spectrum-Customer";

        /// <summary>
        /// The payments key.
        /// </summary>
        private const string PaymentsKey = "Spectrum-Payments";

        /// <summary>
        /// The registration key.
        /// </summary>
        private const string RegistrationKey = "Spectrum-Registration";

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="system">The system.</param>
        /// <returns>
        /// The connection string.
        /// </returns>
        public string GetConnectionString(string system)
        {
            return system;
        }

        /// <summary>
        /// Gets the appointments connection string.
        /// </summary>
        public string AppointmentsConnectionString
        {
            get { return GetConnectionString(AppointmentsKey); }
        }

        /// <summary>
        /// Gets the authentication connection string.
        /// </summary>
        public string AuthenticationConnectionString
        {
            get { return GetConnectionString(AuthenticationKey); }
        }

        /// <summary>
        /// Gets the correspondence connection string.
        /// </summary>
        public string CorrespondenceConnectionString
        {
            get { return GetConnectionString(CorrespondenceKey); }
        }

        /// <summary>
        /// Gets the customer connection string.
        /// </summary>
        public string CustomerConnectionString
        {
            get { return GetConnectionString(CustomerKey); }
        }

        /// <summary>
        /// Gets the payments connection string.
        /// </summary>
        public string PaymentsConnectionString
        {
            get { return GetConnectionString(PaymentsKey); }
        }

        /// <summary>
        /// Gets the registration connection string.
        /// </summary>
        public string RegistrationConnectionString
        {
            get { return GetConnectionString(RegistrationKey); }
        }
    }
}
