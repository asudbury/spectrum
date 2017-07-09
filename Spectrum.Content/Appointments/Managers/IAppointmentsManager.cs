namespace Spectrum.Content.Appointments.Managers
{
    using System.Web;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public interface IAppointmentsManager
    {
        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="httpCookieCollection">The HTTP cookie collection.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        string InsertAppointment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            HttpCookieCollection httpCookieCollection,
            InsertAppointmentViewModel viewModel);
    }
}
