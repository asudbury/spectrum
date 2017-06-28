namespace Spectrum.Content.Appointments.Providers
{
    using ContentModels;
    using Content.Services;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class AppointmentsProvider : IAppointmentsProvider
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsProvider"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public AppointmentsProvider(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the appointments node.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public IPublishedContent GetAppointmentsNode(UmbracoContext umbracoContext)
        {
            return settingsService.GetAppointmentsNode(umbracoContext);
        }

        /// <summary>
        /// Gets the appointments model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public AppointmentsModel GetAppointmentsModel(UmbracoContext umbracoContext)
        {
            return new AppointmentsModel(GetAppointmentsNode(umbracoContext));
        }

        /// <summary>
        /// Gets the google calendar integration.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public string GetGoogleCalendarIntegration(UmbracoContext umbracoContext)
        {
            return GetAppointmentsModel(umbracoContext).GoogleCalendarIntegration;
        }
    }
}