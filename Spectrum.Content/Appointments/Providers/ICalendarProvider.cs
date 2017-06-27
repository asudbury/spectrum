namespace Spectrum.Content.Appointments.Providers
{
    using Umbraco.Web;
    using ViewModels;

    public interface ICalendarProvider
    {
        /// <summary>
        /// Gets the calendar URL.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetCalendarUrl(UmbracoContext umbracoContext);

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="viewModel">The view model.</param>
        void InsertEvent(
            UmbracoContext umbracoContext,
            EventViewModel viewModel);

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        void GetEvents(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="eventId">The event identifier.</param>
        void GetEvent(
            UmbracoContext umbracoContext,
            string eventId);

    }
}
