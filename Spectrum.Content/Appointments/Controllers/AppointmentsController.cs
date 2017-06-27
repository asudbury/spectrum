namespace Spectrum.Content.Appointments.Controllers
{
    using Content.Services;
    using Factories;
    using Models;
    using Providers;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels;

    public class AppointmentsController : BaseController
    {
        /// <summary>
        /// The appointments provider.
        /// </summary>
        private readonly IAppointmentsProvider appointmentsProvider;

        /// <summary>
        /// The calendar factory.
        /// </summary>
        private readonly ICalendarFactory calendarFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="appointmentsProvider">The appointments provider.</param>
        /// <param name="calendarFactory">The calendar factory.</param>
        public AppointmentsController(
            ILoggingService loggingService,
            ISettingsService settingsService,
            IAppointmentsProvider appointmentsProvider,
            ICalendarFactory calendarFactory) 
            : base(loggingService)
        {
            this.appointmentsProvider = appointmentsProvider;
            this.calendarFactory = calendarFactory;
        }

        public AppointmentsController()
            :this(new LoggingService(), 
                 new SettingsService(), 
                 new AppointmentsProvider(), 
                 new CalendarFactory())

        {
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        [HttpPost]
        public void InsertEvent(EventViewModel viewModel)
        {
            calendarFactory.GetCalendarProvider(GetGooleCalendarIntegration())
                .InsertEvent(UmbracoContext, viewModel);
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        [ChildActionOnly]
        public void GetEvent(string eventId)
        {
            calendarFactory.GetCalendarProvider(GetGooleCalendarIntegration())
                .GetEvent(UmbracoContext, eventId);
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        [ChildActionOnly]
        public void GetEvents()
        {
            ICalendarService service = new ICalendarService();

            EventViewModel vm = new EventViewModel();
            vm.StartTime = DateTime.Now;
            vm.EndTime = DateTime.Now.AddHours(3);
            vm.Description = "dddd";
            vm.Summary = "summmmm";
            vm.Attendees = new List<string> { "adria@a.com", "bob@b.com" };

            ICalEventModel model = service.GetICalendarString(vm);

            calendarFactory.GetCalendarProvider(GetGooleCalendarIntegration())
                .GetEvents(UmbracoContext);
        }

        /// <summary>
        /// Gets the goole calendar integration.
        /// </summary>
        /// <returns></returns>
        internal string GetGooleCalendarIntegration()
        {
            return appointmentsProvider.GetGoogleCalendarIntegration(UmbracoContext);
        }
    }
}
