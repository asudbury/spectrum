using System.Web;

namespace Spectrum.Content.Appointments.Managers
{
    using Content.Services;
    using ContentModels;
    using Models;
    using Providers;
    using Translators;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class AppointmentsManager : IAppointmentsManager
    {
        /// <summary>
        /// The logging service.
        /// </summary>
        private readonly ILoggingService loggingService;

        /// <summary>
        /// The appointments provider.
        /// </summary>
        private readonly IAppointmentsProvider appointmentsProvider;

        /// <summary>
        /// The appointment translator.
        /// </summary>
        private readonly IAppointmentTranslator appointmentTranslator;

        /// <summary>
        /// The database provider.
        /// </summary>
        private readonly IDatabaseProvider databaseProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsManager" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="appointmentsProvider">The appointments provider.</param>
        /// <param name="appointmentTranslator">The appointment translator.</param>
        /// <param name="databaseProvider">The database provider.</param>
        public AppointmentsManager(
            ILoggingService loggingService,
            IAppointmentsProvider appointmentsProvider,
            IAppointmentTranslator appointmentTranslator,
            IDatabaseProvider databaseProvider)
        {
            this.loggingService = loggingService;
            this.appointmentsProvider = appointmentsProvider;
            this.appointmentTranslator = appointmentTranslator;
            this.databaseProvider = databaseProvider;
        }

        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="httpCookieCollection">The HTTP cookie collection.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public string InsertAppointment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            HttpCookieCollection httpCookieCollection,
            InsertAppointmentViewModel viewModel)
        {
            bool processed = false;

            PageModel pageModel = new PageModel(publishedContent);

            if (string.IsNullOrEmpty(pageModel.NextPageUrl))
            {
                loggingService.Info(GetType(), "Next Page Url not set");
            }

            if (string.IsNullOrEmpty(pageModel.ErrorPageUrl))
            {
                loggingService.Info(GetType(), "Error Page Url not set");
            }

            AppointmentsModel model = appointmentsProvider.GetAppointmentsModel(umbracoContext);

            AppointmentModel appointmentModel = appointmentTranslator.Translate(viewModel);

            string userName = "OfficeAdmin";

            appointmentModel.CreatedUser = userName;

            if (model.GoogleCalendarIntegration)
            {
                processed = true;    
            }

            if (model.iCalIntegration)
            {
                processed = true;
            }

            if (model.DatabaseIntegration)
            {
                string appointmentId = databaseProvider.InsertAppointment(appointmentModel);

                httpCookieCollection.Add(new HttpCookie("LastAppointmentId", appointmentId));

                processed = true;
            }

            if (processed == false)
            {
                loggingService.Info(GetType(), "No integration setting set to be processed");
                return pageModel.ErrorPageUrl;
            }

            return pageModel.NextPageUrl;
        }
    }
}