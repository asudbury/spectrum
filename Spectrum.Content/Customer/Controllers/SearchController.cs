namespace Spectrum.Content.Customer.Controllers
{
    using Content.Services;
    using Managers;
    using Models;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Umbraco.Web;
    using ViewModels;

    public class SearchController : BaseController
    {
        /// <summary>
        /// The search manager.
        /// </summary>
        private readonly ISearchManager searchManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="searchManager">The search manager.</param>
        /// <inheritdoc />
        public SearchController(
            ILoggingService loggingService,
            ISearchManager searchManager)
            : base(loggingService)
        {
            this.searchManager = searchManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="searchManager">The search manager.</param>
        /// <inheritdoc />
        public SearchController(
            ILoggingService loggingService,
            UmbracoContext umbracoContext,
            ISearchManager searchManager) :
            base(loggingService, umbracoContext)
        {
            this.searchManager = searchManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="searchManager">The search manager.</param>
        /// <inheritdoc />
        public SearchController(
            ILoggingService loggingService,
            UmbracoContext umbracoContext,
            UmbracoHelper umbracoHelper,
            ISearchManager searchManager)
            : base(loggingService, umbracoContext, umbracoHelper)
        {
            this.searchManager = searchManager;
        }

        /// <summary>
        /// Gets the address search.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetAddressSearch()
        {
            return PartialView("Partials/Spectrum/Customer/AddressSearch", new AddressSearchViewModel());
        }

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="addressSearchViewModel">The address search view model.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAddresses(AddressSearchViewModel addressSearchViewModel)
        {
            IEnumerable<AddressModel> models = searchManager.GetAddresses(
                addressSearchViewModel.PostCode,
                addressSearchViewModel.BuildingNumber);

            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}
