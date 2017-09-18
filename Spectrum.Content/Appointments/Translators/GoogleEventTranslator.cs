namespace Spectrum.Content.Appointments.Translators
{
    using Google.Apis.Calendar.v3.Data;
    using ViewModels;

    public class GoogleEventTranslator : IGoogleEventTranslator
    {
        /// <inheritdoc />
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public Event Translate(InsertAppointmentViewModel viewModel)
        {
            return new Event();
        }
    }
}