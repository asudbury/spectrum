namespace Spectrum.Core.Services
{
    public interface ISettingsService
    {
        /// <summary>
        /// Gets a value indicating whether [create spectrum database].
        /// </summary>
        bool CreateSpectrumDatabase { get;  }

        /// <summary>
        /// Gets a value indicating whether [create spectrum SQLCe database type].
        /// </summary>
        bool CreateSQLCeDatabase { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is appointments enabled.
        /// </summary>
        bool IsAppointmentsEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is authentication enabled.
        /// </summary>
        bool IsAuthenticationEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is correspondence enabled.
        /// </summary>
        bool IsCorrespondenceEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is customer enabled.
        /// </summary>
        bool IsCustomerEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is payments enabled.
        /// </summary>
        bool IsPaymentsEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is registration enabled.
        /// </summary>
        bool IsRegistrationEnabled { get; }
    }
}
