namespace Spectrum.Content.Appointments.Controllers
{
    using Content.Services;
    using Providers;
    using System.Web.Mvc;
    using Umbraco.Web;
    using ViewModels;

    public class GoogleCalendarController : BaseController
    {
        /// <summary>
        /// The google calendar provider.
        /// </summary>
        private readonly IGoogleCalendarProvider googleCalendarProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="googleCalendarProvider">The google calendar provider.</param>
        public GoogleCalendarController(
            ILoggingService loggingService,
            IGoogleCalendarProvider googleCalendarProvider) 
            : base(loggingService)
        {
            this.googleCalendarProvider = googleCalendarProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarController"/> class.
        /// </summary>
        public GoogleCalendarController(
            UmbracoContext context, 
            ILoggingService loggingService,
            IGoogleCalendarProvider googleCalendarProvider) 
            : base(context, loggingService)
        {
            this.googleCalendarProvider = googleCalendarProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarController"/> class.
        /// </summary>
        public GoogleCalendarController()
            : this(new LoggingService(), 
                   new GoogleCalendarProvider())
        {
        }

        /// <summary>
        /// Gets the calendar URL.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetCalendarUrl()
        {
            string url = googleCalendarProvider.GetCalendarUrl(UmbracoContext);

            return Content(url);
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        [HttpPost]
        public void InsertEvent(GoogleEventViewModel viewModel)
        {
            googleCalendarProvider.InsertEvent(UmbracoContext, viewModel);
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        public void GetEvent(string eventId)
        {
           googleCalendarProvider.GetEvent(UmbracoContext, eventId);
        }
    }
}
