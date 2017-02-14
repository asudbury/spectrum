namespace Spectrum.Core.Services
{
    public class DatabaseService : IDatabaseService
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="system">The system.</param>
        /// <returns>
        /// The connection string.
        /// </returns>
        public string GetConnectionString(string system)
        {
            string connectionString = system;
            SettingsService settingsService = new SettingsService();
            if (settingsService.CreateSQLCeDatabase)
            {
                connectionString += "-SQLCe";
            }
            return connectionString;
        }

        /// <summary>
        /// Gets the appointments connection string.
        /// </summary>
        public string AppointmentsConnectionString => GetConnectionString(Constants.AppointmentsKey);

        /// <summary>
        /// Gets the authentication connection string.
        /// </summary>
        public string AuthenticationConnectionString => GetConnectionString(Constants.AuthenticationKey);

        /// <summary>
        /// Gets the correspondence connection string.
        /// </summary>
        public string CorrespondenceConnectionString => GetConnectionString(Constants.CorrespondenceKey);

        /// <summary>
        /// Gets the customer connection string.
        /// </summary>
        public string CustomerConnectionString => GetConnectionString(Constants.CustomerKey);

        /// <summary>
        /// Gets the payments connection string.
        /// </summary>
        public string PaymentsConnectionString => GetConnectionString(Constants.PaymentsKey);

        /// <summary>
        /// Gets the registration connection string.
        /// </summary>
        public string RegistrationConnectionString => GetConnectionString(Constants.RegistrationKey);
    }
}
