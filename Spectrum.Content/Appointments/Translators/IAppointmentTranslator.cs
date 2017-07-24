namespace Spectrum.Content.Appointments.Translators
{
    using Models;
    using ViewModels;

    public interface IAppointmentTranslator
    {
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        AppointmentViewModel Translate(
            string paymentsPage,
            AppointmentModel model);
    }
}
