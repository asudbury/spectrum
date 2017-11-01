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

        /// <inheritdoc />
        /// <summary>
        /// Gets the appointments node.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public IPublishedContent GetAppointmentsNode(UmbracoContext umbracoContext)
        {
            return settingsService.GetAppointmentsNode(umbracoContext);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the appointments model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public AppointmentSettingsModel GetAppointmentsModel(UmbracoContext umbracoContext)
        {
            IPublishedContent appointmentsNode =  GetAppointmentsNode(umbracoContext);

            return appointmentsNode != null ? 
                new AppointmentSettingsModel(GetAppointmentsNode(umbracoContext)) : 
                null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        public CustomerModel GetCustomerModel(UmbracoContext umbracoContext)
        {
            IPublishedContent customerNode = settingsService.GetCustomerNode(umbracoContext);

            return  new CustomerModel(customerNode);
        }
    }
}