namespace Spectrum.Content.Appointments.Controllers
{
    using Content.Services;
    using Managers;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class AppointmentsController : BaseController
    {
        /// <summary>
        /// The appointments partial.
        /// </summary>
        private const string AppointmentsPartial = "Partials/Spectrum/Appointments/AppointmentList";

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

            string currentUserName = Members.CurrentUserName;

            string nextUrl = appointmentsManager.InsertAppointment(
                                         UmbracoContext,
                                         publishedContent,
                                         currentUserName,
                                         viewModel);
            
            return Redirect(nextUrl);
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        [ChildActionOnly]
        public void GetAppointment(int appointmentId)
        {
            appointmentsManager.GetAppointment(
                UmbracoContext,
                appointmentId);
        }

        /// <summary>
        /// Gets the future appointments.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetFutureAppointments()
        {
            DateTime dateRangeStart = DateTime.Today;
            DateTime dateRangeEnd = DateTime.Now.AddDays(1000);

            IEnumerable<AppointmentViewModel> appointments = appointmentsManager.GetAppointments(
                UmbracoContext,
                dateRangeStart,
                dateRangeEnd);

            return PartialView(AppointmentsPartial, appointments);
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAppointments(
            DateTime dateRangeStart,
            DateTime dateRangeEnd)
        {
            IEnumerable<AppointmentViewModel> appointments = appointmentsManager.GetAppointments(
                UmbracoContext,
                dateRangeStart,
                dateRangeEnd);

            return PartialView(AppointmentsPartial, appointments);
        }

        /// <summary>
        /// Views the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult View(int id)
        {
            LoggingService.Info(GetType(), "Id=" + id);

            AppointmentViewModel viewModel = appointmentsManager.GetAppointment(UmbracoContext, id);

            return Content("hello from View");
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            LoggingService.Info(GetType(), "Id=" + id);

            AppointmentViewModel viewModel = appointmentsManager.GetAppointment(UmbracoContext, id);

            return Content("hello from Edit");
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            LoggingService.Info(GetType(), "Id=" + id);

            appointmentsManager.DeleteAppointment(UmbracoContext, id);

            return Content("appointment deleted");
        }
    }
}
