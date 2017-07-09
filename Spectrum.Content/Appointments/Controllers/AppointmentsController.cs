namespace Spectrum.Content.Appointments.Controllers
{
    using Content.Services;
    using Managers;
    using Services;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class AppointmentsController : BaseController
    {
        /// <summary>
        /// The appointments manager.
        /// </summary>
        private readonly IAppointmentsManager appointmentsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsManager">The appointments manager.</param>
        public AppointmentsController(
            ILoggingService loggingService,
            IAppointmentsManager appointmentsManager) 
            : base(loggingService)
        {
            this.appointmentsManager = appointmentsManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController"/> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsManager">The appointments manager.</param>
        public AppointmentsController(
            UmbracoContext umbracoContext,
            ILoggingService loggingService,
            IAppointmentsManager appointmentsManager)
            : base(loggingService)
        {
            this.appointmentsManager = appointmentsManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController"/> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsManager">The appointments manager.</param>
        public AppointmentsController(
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ILoggingService loggingService,
            IAppointmentsManager appointmentsManager)
            : base(loggingService)
        {
            this.appointmentsManager = appointmentsManager;
        }
        
        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertAppointment(InsertAppointmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            IPublishedContent publishedContent = GetContentById(CurrentPage.Id.ToString());
            
            string nextUrl = appointmentsManager.InsertAppointment(
                                         UmbracoContext,
                                         publishedContent,
                                         HttpContext.Response.Cookies,
                                         viewModel);
            
            return Redirect(nextUrl);
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        [ChildActionOnly]
        public void GetEvent(string eventId)
        {
            ////calendarFactory.GetCalendarProvider(GetGooleCalendarIntegration())
             ////   .GetEvent(UmbracoContext, eventId);
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        [ChildActionOnly]
        public void GetEvents()
        {
            ICalendarService service = new ICalendarService();
        }
    }
}
