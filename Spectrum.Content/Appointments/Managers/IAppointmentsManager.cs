namespace Spectrum.Content.Appointments.Managers
{
    using Models;
    using System;
    using System.Collections.Generic;
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
        /// <param name="createdUerName">Name of the created uer.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        string InsertAppointment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            string createdUerName,
            InsertAppointmentViewModel viewModel);

        /// <summary>
        /// Gets the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        AppointmentModel GetAppointment(
            UmbracoContext umbracoContext,
            int appointmentId);
        
        /// <summary>
        /// Gets the appointments.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        IEnumerable<AppointmentModel> GetAppointments(
            UmbracoContext umbracoContext,
            DateTime dateRangeStart,
            DateTime dateRangeEnd);
    }
}
