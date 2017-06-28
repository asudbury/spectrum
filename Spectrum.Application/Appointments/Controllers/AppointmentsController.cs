namespace Spectrum.Application.Appointments.Controllers
{
    using Correspondence.Providers;
    using Core.Services;
    using Providers;

    public class AppointmentsController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The appointments provider.
        /// </summary>
        private readonly IAppointmentsProvider appointmentsProvider;

        /// <summary>
        /// The event provider.
        /// </summary>
        private readonly IEventProvider eventProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="appointmentsProvider">The appointments provider.</param>
        /// <param name="eventProvider">The event provider.</param>
        public AppointmentsController(
            ISettingsService settingsService,
            IAppointmentsProvider appointmentsProvider,
            IEventProvider eventProvider)
        {
            this.settingsService = settingsService;
            this.appointmentsProvider = appointmentsProvider;
            this.eventProvider = eventProvider;
        }
    }
}
