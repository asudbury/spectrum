namespace Spectrum.Database
{
    using Core.Services;
    using Registration.Controllers;

    public class Bootstrap
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrap"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public Bootstrap(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            //// check that we want to bootstrap the database
            if (settingsService.CreateSpectrumDatabase)
            {
                //// each individual controller works out if the bootstrapping needs to be done.
                RegistrationController registrationController = new RegistrationController();
                registrationController.Bootstrap();
            }
        }
    }
}
