namespace Spectrum.Content.Navigation.Managers
{
    using System.Collections.Generic;
    using ContentModels;
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
        /// The rules engine service.
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainNavigationManager" /> class.
        /// </summary>
        /// <param name="mainNavigationProvider">The main navigation provider.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        public MainNavigationManager(
            IMainNavigationProvider mainNavigationProvider,
            IUserService userService,
            ISettingsService settingsService,
            IRulesEngineService rulesEngineService)
        {
            this.mainNavigationProvider = mainNavigationProvider;
            this.userService = userService;
            this.settingsService = settingsService;
            this.rulesEngineService = rulesEngineService;
        }

        /// <inheritdoc />
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
                //// CustomerAdmin is the one we have started with.

                menu = userService.GetDefaultRole();

                if (menu == string.Empty)
                {
                    menu = "DefaultLoggedIn";
                }
            }

            IPublishedContent menuNode = settingsService.GetMenu(menu);

            if (menuNode != null)
            {
                IEnumerable<MenuItemModel> menuItems = mainNavigationProvider.GetMenu(menuNode);

                List<MenuItemModel> allowedMenuItems = new List<MenuItemModel>();

                foreach (MenuItemModel menuItemModel in menuItems)
                {
                    if (string.IsNullOrEmpty(menuItemModel.DisplayRule) == false)
                    {
                        bool result = rulesEngineService.Execute(menuItemModel.DisplayRule);

                        if (result)
                        {
                            allowedMenuItems.Add(menuItemModel);
                        }
                    }

                    else
                    {
                        allowedMenuItems.Add(menuItemModel);
                    }
                }

                MenuViewModel viewModel = new MenuViewModel
                {
                    MenuItems = allowedMenuItems
                };

                return viewModel;
            }

            return new MenuViewModel();
        }
    }
}