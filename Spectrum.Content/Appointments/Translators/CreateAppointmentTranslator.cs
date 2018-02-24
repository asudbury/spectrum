namespace Spectrum.Content.Appointments.Translators
{
    using Models;
    using Scorchio.Services;
    using System;
    using ViewModels;

    public class CreateAppointmentTranslator : ICreateAppointmentTranslator
    {
        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAppointmentTranslator"/> class.
        /// </summary>
        public CreateAppointmentTranslator(IEncryptionService encryptionService)
        {
            this.encryptionService = encryptionService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public AppointmentModel Translate(CreateAppointmentViewModel viewModel)
        {
            DateTime startTime = viewModel.StartTime.ToUniversalTime();
            DateTime now = DateTime.Now.ToUniversalTime();
            
            AppointmentModel model = new AppointmentModel
            {
                CreatedTime = now,
                ClientId = Convert.ToInt32(encryptionService.DecryptString(viewModel.ClientId)),
                LastUpdatedTime = now,
                StartTime = startTime,
                Duration = viewModel.Duration,
                Description = viewModel.Description,
                Location = viewModel.Location,
                ServiceProviderId = 0
            };
            
            return model;
        }
    }
}