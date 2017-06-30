namespace Spectrum.Content.Navigation.Managers
{
    using Providers;
    using Services;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;
    
    public class MainNavigationManager : IMainNavigationManager
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The main navigation provider.
        /// </summary>
        private readonly IMainNavigationProvider mainNavigationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainNavigationManager"/> class.
        /// </summary>
        /// <param name="mainNavigationProvider">The main navigation provider.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="settingsService">The settings service.</param>
        public MainNavigationManager(
            IMainNavigationProvider mainNavigationProvider, 
            IUserService userService, 
            ISettingsService settingsService)
        {
            this.mainNavigationProvider = mainNavigationProvider;
            this.userService = userService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the menu view model.
        /// </summary>
        /// <returns></returns>
        public MenuViewModel GetMenuViewModel()
        {
            string menu = "Default";

            if (userService.IsUserLoggedIn())
            {
                //// use the name of the role to be the lookup of the menu in umbraco
                //// OfficeAdmin is the one we have started with.

                menu = userService.GetDefaultRole();

                if (menu == string.Empty)
                {
                    menu = "DefaultLoggedIn";
                }
            }

            IPublishedContent menuNode = settingsService.GetMenu(UmbracoContext.Current, menu);

            if (menuNode != null)
            {
                MenuViewModel viewModel = new MenuViewModel
                {
                    MenuItems = mainNavigationProvider.GetMenu(menuNode)
                };

                return viewModel;
            }

            return new MenuViewModel();
        }
    }
}