namespace Spectrum.Content.Appointments.Providers
{
    using ContentModels;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public interface IAppointmentsProvider
    {
        /// <summary>
        /// Gets the appointments node.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        IPublishedContent GetAppointmentsNode(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the appointments model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        AppointmentSettingsModel GetAppointmentsModel(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the customer model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        CustomerModel GetCustomerModel(UmbracoContext umbracoContext);
    }
}
