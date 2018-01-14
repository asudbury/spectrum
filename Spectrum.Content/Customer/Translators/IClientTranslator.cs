namespace Spectrum.Content.Customer.Translators
{
    using Models;
    using ViewModels;

    public interface IClientTranslator
    {
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        ClientModel Translate(CreateClientViewModel viewModel);

        /// <summary>
        /// Translates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ClientViewModel Translate(ClientModel model);
    }
}