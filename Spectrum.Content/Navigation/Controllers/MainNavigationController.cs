namespace Spectrum.Content.Navigation.Controllers
{
    using Managers;
    using Services;
    using System.Web.Mvc;
    using Umbraco.Web;
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

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="mainNavigationManager">The main navigation manager.</param>
        public MainNavigationController(
            UmbracoContext umbracoContext,
            ILoggingService loggingService,
            IMainNavigationManager mainNavigationManager)
            : base(loggingService,
                   umbracoContext)
        {
            this.mainNavigationManager = mainNavigationManager;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="mainNavigationManager">The main navigation manager.</param>
        public MainNavigationController(
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ILoggingService loggingService,
            IMainNavigationManager mainNavigationManager)
            : base(loggingService,
                   umbracoContext,
                   umbracoHelper)
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
