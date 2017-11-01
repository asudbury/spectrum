namespace Spectrum.Content.Appointments.Controllers
{
    using Content.Models;
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
        /// The rules engine service.
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Appointments.Controllers.AppointmentsController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsManager">The appointments manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public AppointmentsController(
            ILoggingService loggingService,
            IAppointmentsManager appointmentsManager,
            IRulesEngineService rulesEngineService) 
            : base(loggingService)
        {
            this.appointmentsManager = appointmentsManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Appointments.Controllers.AppointmentsController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsManager">The appointments manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public AppointmentsController(
            UmbracoContext umbracoContext,
            ILoggingService loggingService,
            IAppointmentsManager appointmentsManager,
            IRulesEngineService rulesEngineService)
            : base(loggingService)
        {
            this.appointmentsManager = appointmentsManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Appointments.Controllers.AppointmentsController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsManager">The appointments manager.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        /// <inheritdoc />
        public AppointmentsController(
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ILoggingService loggingService,
            IAppointmentsManager appointmentsManager,
            IRulesEngineService rulesEngineService)
            : base(loggingService)
        {
            this.appointmentsManager = appointmentsManager;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Appointmentses this instance.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Appointments()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerAppointmentsEnabled(UmbracoContext))
            {
                return PartialView("");
            }

            return default(PartialViewResult);
        }

        /// <summary>
        /// Appointments the specified view model.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult InsertAppointment()
        {
            LoggingService.Info(GetType());

            if (rulesEngineService.IsCustomerAppointmentsEnabled(UmbracoContext))
            {
                return PartialView("", new InsertAppointmentViewModel());
            }

            return default(PartialViewResult);
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
                                         viewModel,
                                         Members.CurrentUserName);
            
            return Redirect(nextUrl);
        }

        /// <summary>
        /// Gets the appointment.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        [ChildActionOnly]
        public void GetAppointment(string appointmentId)
        {
            LoggingService.Info(GetType());

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
            LoggingService.Info(GetType());

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
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBootGridAppointments(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems)
        {
            LoggingService.Info(GetType());

            DateTime dateRangeStart = DateTime.Now.AddDays(-10000);
            DateTime dateRangeEnd = DateTime.Now.AddDays(10000);

            BootGridViewModel<AppointmentViewModel> bootGridViewModel = appointmentsManager.GetBootGridAppointments(
                                                                            current,
                                                                            rowCount,
                                                                            searchPhrase,
                                                                            sortItems,
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
        /// Views the appointment.
        /// </summary>
        /// <param name="beeswwr">The beeswwr. (the appoinment id!!!)</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult ViewAppointment(string beeswwr)
        {
            LoggingService.Info(GetType(), "beeswwr=" + beeswwr);

            AppointmentViewModel viewModel = appointmentsManager.GetAppointment(UmbracoContext, beeswwr);

            return PartialView("Partials/Spectrum/Appointments/Appointment", viewModel);
        }

        /// <summary>
        /// Gets the updated appointment.
        /// </summary>
        /// <param name="beeswwr">The beeswwr.</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult GetUpdatedAppointment(string beeswwr)
        {
            LoggingService.Info(GetType(), "Id=" + beeswwr);

            AppointmentViewModel viewModel = appointmentsManager.GetAppointment(UmbracoContext, beeswwr);

            return PartialView("Partials/Spectrum/Appointments/UpdateAppointment", viewModel);
        }

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAppointment(AppointmentViewModel viewModel)
        {
            LoggingService.Info(GetType());

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            string nextUrl = appointmentsManager.UpdateAppointment(
                UmbracoContext,
                viewModel);

            return Redirect(nextUrl);
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

            string url = appointmentsManager.DeleteAppointment(UmbracoContext, id);

            return Content(url);
        }
    }
}
