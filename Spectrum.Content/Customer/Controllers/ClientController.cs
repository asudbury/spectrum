namespace Spectrum.Content.Customer.Controllers
{
    using Components.ViewModels;
    using Content.Models;
    using Content.Services;
    using Managers;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels;

    public class ClientController : BaseController
    {
        /// <summary>
        /// The client manager.
        /// </summary>
        private readonly IClientManager clientManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="clientManager">The client manager.</param>
        /// <inheritdoc />
        public ClientController(
            ILoggingService loggingService,
            IClientManager clientManager) 
            : base(loggingService)
        {
            this.clientManager = clientManager;
        }

        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        /// <param name="fkdssre">The client id.</param>
        /// <param name="wsqdfff">The customer id.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ClientName(
            string fkdssre,
            string wsqdfff)
        {
            if (string.IsNullOrEmpty(fkdssre) == false)
            {
                LinkViewModel viewModel = clientManager.GetClientName(
                                                    fkdssre, 
                                                    wsqdfff);

                if (viewModel != null)
                {
                    return PartialView(Constants.Partials.NoLink, viewModel);
                }
            }

            return Content(string.Empty);
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetClients()
        {
            LoggingService.Info(GetType());

            return PartialView(Constants.Partials.Clients);
        }
        
        /// <summary>
        /// Gets the boot grid clients.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBootGridClients(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems)
        {
            LoggingService.Info(GetType());

            string jsonString = clientManager.GetBootGridClients(
                current,
                rowCount,
                searchPhrase,
                sortItems,
                UmbracoContext);

            return Content(jsonString, "application/json");
        }
        
        /// <summary>
        /// Gets the Create a Client.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult GetCreateClient()
        {
            LoggingService.Info(GetType());

            return PartialView(Constants.Partials.CreateClient, new CreateClientViewModel());
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClient(CreateClientViewModel viewModel)
        {
            LoggingService.Info(GetType());

            bool inUse = clientManager.CheckNameInUse(viewModel.Name);

            if (inUse)
            {
                ModelState.AddModelError("Name", "There is already a client with this Name");
            }

            inUse = clientManager.CheckEmailInUse(viewModel.EmailAddress);

            if (inUse)
            {
                ModelState.AddModelError("EmailAddress", "There is already a client with this Email Address");
            }

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            string url = clientManager.CreateClient(viewModel);
            
            return Redirect(url);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateClient(ClientViewModel viewModel)
        {
            LoggingService.Info(GetType());

            /* This doesnt work if the client doesnt update these values - need to take a look!
            bool inUse = clientManager.CheckNameInUse(viewModel.Name);

            if (inUse)
            {
                ModelState.AddModelError("Name", "There is already a client with this Name");
            }

            inUse = clientManager.CheckEmailInUse(viewModel.EmailAddress);

            if (inUse)
            {
                ModelState.AddModelError("EmailAddress", "There is already a client with this Email Address");
            }
            */

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            string url = clientManager.UpdateClient(viewModel);

            return Redirect(url);
        }

        /// <summary>
        /// Views the client.
        /// </summary>
        /// <param name="fkdssre">The fkdssre. (encryptred client id)</param>
        /// <returns></returns>
        public PartialViewResult ViewClient(string fkdssre)
        {
            LoggingService.Info(GetType());

            ClientViewModel viewModel = clientManager.GetClient(fkdssre);

            return PartialView(Constants.Partials.ViewClient, viewModel);
        }

        /// <summary>
        /// Views the client.
        /// </summary>
        /// <param name="fkdssre">The fkdssre. (encryptred client id)</param>
        /// <returns></returns>
        public PartialViewResult GetClientHeader(string fkdssre)
        {
            LoggingService.Info(GetType());

            ClientViewModel viewModel = clientManager.GetClient(fkdssre);

            return PartialView(Constants.Partials.ClientHeader, viewModel);
        }

        /// <summary>
        /// Get the update client.
        /// </summary>
        /// <param name="fkdssre">The fkdssre. (encryptred client id)</param>
        /// <returns></returns>
        public PartialViewResult GetUpdateClient(string fkdssre)
        {
            LoggingService.Info(GetType());

            ClientViewModel viewModel = clientManager.GetClient(fkdssre);

            return PartialView(Constants.Partials.UpdateClient, viewModel);
        }
    }
}
