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

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Appointments.Controllers.GoogleCalendarController" /> class.
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
            LoggingService.Info(GetType());

            AppointmentSettingsModel model = appointmentsProvider.GetAppointmentsModel(UmbracoContext);

            if (model != null)
            {
                if (model.GoogleCalendarEnabled && 
                    string.IsNullOrEmpty(model.GoogleCalendarUrl) == false)
                {
                    return Content(model.GoogleCalendarUrl);
                }
            }

            return Content("");
        }
    }
}
