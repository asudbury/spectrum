namespace Spectrum.Content.Appointments.Translators
{
    using Models;
    using ViewModels;

    public class AppointmentTranslator : IAppointmentTranslator
    {
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AppointmentViewModel Translate(AppointmentModel model)
        {
            AppointmentViewModel viewModel = new AppointmentViewModel
            {
                Id = model.Id,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Status =  ((AppointmentStatus)model.Status).ToString(),
                PaymentId = model.PaymentId,
                Location = model.Location,
                Description = model.Description
            };


            return viewModel;
        }
    }
}