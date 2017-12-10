namespace Spectrum.Content.Payments.Managers
{
    using Content.Models;
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public interface IPaymentManager
    {
        string GetCustomerName(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetAuthToken(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetEnvironment(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the transaction view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="encryptedId">The encrypted identifier.</param>
        /// <returns></returns>
        TransactionViewModel GetTransactionViewModel(
            UmbracoContext umbracoContext,
            string encryptedId);

        /// <summary>
        /// Gets the transactions view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        IEnumerable<TransactionViewModel> GetTransactionsViewModel(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the boot grid transactions.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetBootGridTransactions(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext);

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns></returns>
        string MakePayment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            MakePaymentViewModel viewModel,
            string currentUser);
    }
}
