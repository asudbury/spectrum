namespace Spectrum.Content.Navigation.Controllers
{
    using Providers;
    using System.Web.Mvc;
    using Services;
    using ViewModels;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class MainNavigationController : BaseController
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
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="mainNavigationProvider">The main navigation provider.</param>
        public MainNavigationController(
            ILoggingService loggingService,
            ISettingsService settingsService,
            IUserService userService,
            IMainNavigationProvider mainNavigationProvider) 
            : base(loggingService)
        {
            this.settingsService = settingsService;
            this.userService = userService;
            this.mainNavigationProvider = mainNavigationProvider;
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetMenu()
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

                return PartialView("Partials/Spectrum/Navigation/Menu", viewModel);
            }

            return PartialView("Partials/Spectrum/Navigation/Menu", new MenuViewModel());
        }
    }
}
