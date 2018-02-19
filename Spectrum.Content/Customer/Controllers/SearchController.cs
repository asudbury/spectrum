namespace Spectrum.Content.Customer.Controllers
{
    using Content.Services;
    using Managers;
    using Scorchio.PostalAddressSearch.Models;
    using System.Collections.Generic;
    using System.Web.Mvc;
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
