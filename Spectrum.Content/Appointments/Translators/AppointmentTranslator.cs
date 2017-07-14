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
            AppointmentViewModel viewModel = new AppointmentViewModel();

            return viewModel;
        }
    }
}