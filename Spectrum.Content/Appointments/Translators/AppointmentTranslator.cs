namespace Spectrum.Content.Appointments.Translators
{
    using Application.Services;
    using Models;
    using ViewModels;

    public class AppointmentTranslator : IAppointmentTranslator
    {
        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentTranslator"/> class.
        /// </summary>
        /// <param name="encryptionService">The encryption service.</param>
        public AppointmentTranslator(IEncryptionService encryptionService)
        {
            this.encryptionService = encryptionService;
        }

        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AppointmentViewModel Translate(
            string paymentsPage,
            AppointmentModel model)
        {
            AppointmentViewModel viewModel = new AppointmentViewModel
            {
                Id = model.Id,
                EncryptedId = encryptionService.EncryptString(model.Id.ToString()),
                CreatedTime = model.CreatedTime,
                CreatedUser = model.CreatedUser,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Status = ((AppointmentStatus)model.Status).ToString(),
                PaymentId = model.PaymentId,
                Location = model.Location,
                Description = model.Description,
                TakePaymentUrl = BuildPaymentsUrl(paymentsPage, model.Id)
            };

            return viewModel;
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
                url = paymentsPage + "?appointmentId=" + encryptionService.EncryptString(appointmentId.ToString());
            }

            return url;

        }
    }
}