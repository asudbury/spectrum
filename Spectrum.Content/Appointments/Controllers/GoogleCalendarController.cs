namespace Spectrum.Content.Appointments.Controllers
{
    using ContentModels;
    using System.Web.Mvc;
    using Services;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class GoogleCalendarController : BaseController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        public GoogleCalendarController(
            ILoggingService loggingService,
            ISettingsService settingsService) 
            : base(loggingService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarController"/> class.
        /// </summary>
        public GoogleCalendarController(
            UmbracoContext context, 
            ILoggingService loggingService,
            ISettingsService settingsService) 
            : base(context, loggingService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the calendar URL.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetCalendarUrl()
        {
            IPublishedContent content = settingsService.GetAppointmentsNode(UmbracoContext);

            AppointmentsModel model = new AppointmentsModel(content);

            return Content(model.GoogleCalendarUrl);
        }
    }
}
