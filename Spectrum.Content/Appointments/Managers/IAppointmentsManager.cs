namespace Spectrum.Content.Appointments.Managers
{
    using Content.Models;
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
        AppointmentViewModel GetAppointment(
            UmbracoContext umbracoContext,
            string appointmentId);
        
        /// <summary>
        /// Gets the appointments.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        IEnumerable<AppointmentViewModel> GetAppointments(
            UmbracoContext umbracoContext,
            DateTime dateRangeStart,
            DateTime dateRangeEnd);

        /// <summary>
        /// Gets the boot grid appointments.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        BootGridViewModel<AppointmentViewModel> GetBootGridAppointments(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext,
            DateTime dateRangeStart,
            DateTime dateRangeEnd);

        /// <summary>
        /// Deletes the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        bool DeleteAppointment(
            UmbracoContext umbracoContext,
            string appointmentId);

        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        string UpdateAppointment(
            UmbracoContext umbracoContext,
            AppointmentViewModel viewModel);
    }
}
