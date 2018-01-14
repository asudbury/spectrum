namespace Spectrum.Content.Appointments.Translators
{
    using Application.Services;
    using Content.Services;
    using Models;
    using System;
    using ViewModels;

    public class AppointmentTranslator : IAppointmentTranslator
    {
        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// The URL service.
        /// </summary>
        private readonly IUrlService urlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentTranslator" /> class.
        /// </summary>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="urlService">The URL service.</param>
        public AppointmentTranslator(
            IEncryptionService encryptionService,
            IUrlService urlService)
        {
            this.encryptionService = encryptionService;
            this.urlService = urlService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AppointmentViewModel Translate(
            string paymentsPage,
            ClientAppointmentModel model)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(model.StartTime);

            AppointmentViewModel viewModel = new AppointmentViewModel
            {
                Id = model.Id,
                EncryptedId = encryptionService.EncryptString(model.Id.ToString()),
                CreatedTime = TimeZone.CurrentTimeZone.ToLocalTime(model.CreatedTime),
                CreatedUser = model.CreatedUser,
                LastUpdatedTime = TimeZone.CurrentTimeZone.ToLocalTime(model.LastUpdatedTime),
                LastedUpdatedUser = model.LastUpdatedUser,
                StartTime = startTime,
                Duration = model.Duration,
                Status = GetAppointmentStatus(model.Status, model.StartTime),
                InvoiceId = model.InvoiceId,
                Location = model.Location,
                Description = model.Description,
                ViewAppointmentUrl = urlService.GetViewAppointmentUrl(model.ClientId, model.Id),
                UpdateAppointmentUrl = urlService.GetUpdateAppointmentUrl(model.ClientId, model.Id),
                DeleteAppointmentUrl = "/umbraco/Surface/Appointments/Delete/" + encryptionService.EncryptString(model.Id.ToString()),
                GoogleSearchUrl = urlService.GetGoogleSearchUrl(model.Location),
                ClientUrl = urlService.GetViewClientUrl(model.ClientId),
                ClientName = encryptionService.DecryptString(model.ClientName)
            };

            return viewModel;
        }

        /// <inheritdoc />
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public AppointmentModel Translate(AppointmentViewModel viewModel)
        {
            AppointmentModel model = new AppointmentModel
            {
                Id = viewModel.Id,
                CreatedTime = viewModel.CreatedTime,
                CreatedUser = viewModel.CreatedUser,
                LastUpdatedTime = viewModel.LastUpdatedTime,
                LastUpdatedUser = viewModel.LastedUpdatedUser,
                StartTime = viewModel.StartTime,
                Duration = viewModel.Duration,
                Location = viewModel.Location,
                Description = viewModel.Description,
                InvoiceId = viewModel.InvoiceId
            };

            return model;
        }
        
        /// <summary>
        /// Translates the specified original model.
        /// </summary>
        /// <param name="originalModel">The original model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public AppointmentModel Translate(
            AppointmentModel originalModel,
            UpdateAppointmentViewModel viewModel)
        {
            DateTime now = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(viewModel.StartTime);
            
            AppointmentModel model = new AppointmentModel
            {
                Id = originalModel.Id,
                ClientId = originalModel.ClientId,
                CustomerId = originalModel.CustomerId,
                ServiceProviderId = originalModel.ServiceProviderId,
                CreatedTime = originalModel.CreatedTime,
                CreatedUser = originalModel.CreatedUser,
                LastUpdatedTime = now,
                LastUpdatedUser = originalModel.CreatedUser,
                StartTime = startTime,
                Duration = viewModel.Duration,
                Location = viewModel.Location,
                Description = viewModel.Description,
                InvoiceId =  originalModel.InvoiceId
            };

            return model;
        }

        /// <summary>
        /// Gets the appointment status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="appointmentTime">The appointment time.</param>
        /// <returns></returns>
        internal string GetAppointmentStatus(
            int status,
            DateTime appointmentTime)
        {
            if ((AppointmentStatus)status != AppointmentStatus.Unknown)
            {
                return ((AppointmentStatus)status).ToString();
            }

            return appointmentTime > DateTime.Now ? "Outstanding" : "Complete";
        }

        /// <summary>
        /// Builds the appointment URL.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        internal string BuildAppointmentUrl(
            string paymentsPage,
            int appointmentId,
            string page)
        {
            string url = string.Empty;

            if (string.IsNullOrEmpty(paymentsPage) == false)
            {
                //// TODO : this is rubbish but will do for now!
                url = paymentsPage.Replace("payments/pay", "appointments/" + page) + "?" + Constants.QueryString.AppointmentId + "=" + encryptionService.EncryptString(appointmentId.ToString());
            }

            return url;
        }

        /// <summary>
        /// Builds the payments URL.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        internal string BuildPaymentsUrl(
            string paymentsPage,
            int appointmentId)
        {
            string url = string.Empty;

            if (string.IsNullOrEmpty(paymentsPage) == false)
            {
                url = paymentsPage + "?" + Constants.QueryString.AppointmentId + "=" + encryptionService.EncryptString(appointmentId.ToString());
            }

            return url;
        }
    }
}