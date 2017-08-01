namespace Spectrum.Content.Appointments.Controllers
{
    using Content.Services;
    using Managers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
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
        public void GetAppointment(string appointmentId)
        {
            appointmentsManager.GetAppointment(
                UmbracoContext,
                appointmentId);
        }

        /// <summary>
        /// Gets the appointments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAppointments()
        {
            DateTime dateRangeStart = DateTime.Now.AddDays(-10000);
            DateTime dateRangeEnd = DateTime.Now.AddDays(10000);

            IEnumerable<AppointmentViewModel> viewModels = appointmentsManager.GetAppointments(
                UmbracoContext,
                dateRangeStart,
                dateRangeEnd);

            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the boot grid appointments.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBootGridAppointments()
        {
            DateTime dateRangeStart = DateTime.Now.AddDays(-10000);
            DateTime dateRangeEnd = DateTime.Now.AddDays(10000);

            BootGridViewModel<AppointmentViewModel> bootGridViewModel = appointmentsManager.GetBootGridAppointments(
                                                                            UmbracoContext,
                                                                            dateRangeStart,
                                                                            dateRangeEnd);

            string jsonString = JsonConvert.SerializeObject(
                                    bootGridViewModel,
                                    new JsonSerializerSettings
                                    {
                                        ContractResolver = new CamelCasePropertyNamesContractResolver()

                                    });

            return Content(jsonString, "application/json");
        }

        /// <summary>
        /// Views the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult View(string id)
        {
            LoggingService.Info(GetType(), "Id=" + id);

            AppointmentViewModel viewModel = appointmentsManager.GetAppointment(UmbracoContext, id);

            return PartialView("Partials/Spectrum/Appointments/Appointment", viewModel);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult Edit(string id)
        {
            LoggingService.Info(GetType(), "Id=" + id);

            AppointmentViewModel viewModel = appointmentsManager.GetAppointment(UmbracoContext, id);

            return PartialView("Partials/Spectrum/Appointments/Appointment", viewModel);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(string id)
        {
            LoggingService.Info(GetType(), "Id=" + id);

            appointmentsManager.DeleteAppointment(UmbracoContext, id);

            return Content("appointment deleted");
        }
    }
}
