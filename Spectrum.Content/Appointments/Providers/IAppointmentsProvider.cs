using Spectrum.Content.ContentModels;

namespace Spectrum.Content.Appointments.Providers
{
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

        AppointmentsModel GetAppointmentsModel(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the google calendar integration.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetGoogleCalendarIntegration(UmbracoContext umbracoContext);
    }
}
