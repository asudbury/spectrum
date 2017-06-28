namespace Spectrum.Content.Appointments.Controllers
{
    using ContentModels;
    using Content.Services;
    using Providers;
    using System.Web.Mvc;

    public class GoogleCalendarController : BaseController
    {
        /// <summary>
        /// The appointments provider.
        /// </summary>
        private readonly IAppointmentsProvider appointmentsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsProvider">The appointments provider.</param>
        public GoogleCalendarController(
            ILoggingService loggingService,
            IAppointmentsProvider appointmentsProvider)
            : base(loggingService)
        {
            this.appointmentsProvider = appointmentsProvider;
        }

        /// <summary>
        /// Gets the calendar URL.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetCalendarUrl()
        {
            AppointmentsModel model = appointmentsProvider.GetAppointmentsModel(UmbracoContext);

            return Content(model.GoogleCalendarUrl);
        }
    }
}
