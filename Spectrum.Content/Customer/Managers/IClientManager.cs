namespace Spectrum.Content.Customer.Managers
{
    using Components.ViewModels;
    using Content.Models;
    using System.Collections.Generic;
    using Umbraco.Web;
    using ViewModels;

    public interface IClientManager
    {
        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        string CreateClient(CreateClientViewModel viewModel);

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="encryptedId">The encrypted identifier.</param>
        /// <returns></returns>
        ClientViewModel GetClient(string encryptedId);

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ClientViewModel> GetClients();

        /// <summary>
        /// Gets the boot grid clients.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetBootGridClients(int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext);

        /// <summary>
        /// Checks the name in use.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        bool CheckNameInUse(string name);

        /// <summary>
        /// Checks the email in use.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        bool CheckEmailInUse(string emailAddress);

        /// <summary>
        /// Gets the name of the client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        LinkViewModel GetClientName(string clientId);
    }
}