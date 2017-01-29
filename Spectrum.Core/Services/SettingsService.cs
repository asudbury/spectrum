namespace Spectrum.Core.Services
{
    using System.Collections.Specialized;
    using System.Configuration;

    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// The spectrum settings.
        /// </summary>
        private static readonly NameValueCollection SpectrumSettings = ConfigurationManager.GetSection(Constants.SpectrumConfigSectionName) as NameValueCollection;

        /// <summary>
        /// Gets a value indicating whether this instance is appointments enabled.
        /// </summary>
        public bool IsAppointmentsEnabled => GetBoolSetting(Constants.AppointmentsKey + Constants.EnabledKey);

        /// <summary>
        /// Gets a value indicating whether this instance is authentication enabled.
        /// </summary>
        public bool IsAuthenticationEnabled => GetBoolSetting(Constants.AuthenticationKey + Constants.EnabledKey);
        
        /// <summary>
        /// Gets a value indicating whether this instance is correspondence enabled.
        /// </summary>
        public bool IsCorrespondenceEnabled => GetBoolSetting(Constants.CorrespondenceKey + Constants.EnabledKey);

        /// <summary>
        /// Gets a value indicating whether this instance is customer enabled.
        /// </summary>
        public bool IsCustomerEnabled => GetBoolSetting(Constants.CustomerKey + Constants.EnabledKey);
        
        /// <summary>
        /// Gets a value indicating whether this instance is payments enabled.
        /// </summary>
        public bool IsPaymentsEnabled => GetBoolSetting(Constants.PaymentsKey + Constants.EnabledKey);

        /// <summary>
        /// Gets a value indicating whether this instance is registration enabled.
        /// </summary>
        public bool IsRegistrationEnabled => GetBoolSetting(Constants.RegistrationKey + Constants.EnabledKey);
        
        /// <summary>
        /// Gets the bool setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        internal bool GetBoolSetting(string key)
        {
            bool result;

            string setting = SpectrumSettings[key];

            bool.TryParse(setting, out result);

            return result;
        }
    }
}
