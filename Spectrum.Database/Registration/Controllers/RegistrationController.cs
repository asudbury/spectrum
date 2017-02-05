namespace Spectrum.Database.Registration.Controllers
{
    using Core.Services;
    using Providers;

    public class RegistrationController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The registration provider.
        /// </summary>
        private readonly IRegistrationProvider registrationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="registrationProvider">The registration provider.</param>
        public RegistrationController(
            ISettingsService settingsService,
            IRegistrationProvider registrationProvider)
            : base()
        {
            this.settingsService = settingsService;
            this.registrationProvider = registrationProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        public RegistrationController()
            : this(new SettingsService(), new RegistrationProvider())
        {
        }

        /// <summary>
        /// </summary>
        public void Bootstrap()
        {
            if (settingsService.IsRegistrationEnabled)
            {
                registrationProvider.Bootstrap();
            }
        }
    }
}
