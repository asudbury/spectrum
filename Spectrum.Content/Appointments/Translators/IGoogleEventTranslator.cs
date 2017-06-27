namespace Spectrum.Content.Appointments.Translators
{
    using Google.Apis.Calendar.v3.Data;
    using ViewModels;

    public interface IGoogleEventTranslator
    {
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        Event Translate(EventViewModel viewModel);
    }
}
