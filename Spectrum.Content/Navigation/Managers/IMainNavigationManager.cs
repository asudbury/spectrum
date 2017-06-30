namespace Spectrum.Content.Navigation.Managers
{
    using ViewModels;

    public interface IMainNavigationManager
    {
        /// <summary>
        /// Gets the menu view model.
        /// </summary>
        /// <returns></returns>
        MenuViewModel GetMenuViewModel();
    }
}
