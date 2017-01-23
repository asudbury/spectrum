namespace Spectrum.Core.Services
{
    /// <summary>
    /// The Database Service interface.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="system">The system.</param>
        /// <returns>The connection string.</returns>
        string GetConnectionString(string system);

        /// <summary>
        /// Gets the appointments connection string.
        /// </summary>
        string AppointmentsConnectionString { get; }

        /// <summary>
        /// Gets the authentication connection string.
        /// </summary>
        string AuthenticationConnectionString { get;  }

        /// <summary>
        /// Gets the correspondence connection string.
        /// </summary>
        string CorrespondenceConnectionString { get; }

        /// <summary>
        /// Gets the customer connection string.
        /// </summary>
        string CustomerConnectionString { get; }

        /// <summary>
        /// Gets the payments connection string.
        /// </summary>
        string PaymentsConnectionString { get; }

        /// <summary>
        /// Gets the registration connection string.
        /// </summary>
        string RegistrationConnectionString { get; }
    }
}
