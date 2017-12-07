namespace Spectrum.Content.Navigation.Controllers
{
    using Managers;
    using Services;
    using System.Web.Mvc;
    using ViewModels;

    public class MainNavigationController : BaseController
    {
        /// <summary>
        /// The main navigation manager.
        /// </summary>
        private readonly IMainNavigationManager mainNavigationManager;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="mainNavigationManager">The main navigation manager.</param>
        public MainNavigationController(
            ILoggingService loggingService,
            IMainNavigationManager mainNavigationManager) 
            :base(loggingService)
        {
            this.mainNavigationManager = mainNavigationManager;
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetMenu()
        {
            MenuViewModel viewModel = mainNavigationManager.GetMenuViewModel();

            return PartialView("Partials/Spectrum/Navigation/Menu", viewModel);
        }
    }
}
