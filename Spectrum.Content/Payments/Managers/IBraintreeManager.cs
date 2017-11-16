namespace Spectrum.Content.Payments.Managers
{
    using Content.Models;
    using System.Collections.Generic;
    using Umbraco.Web;
    using ViewModels;

    public interface IBraintreeManager
    {
        /// <summary>
        /// Gets the transactions view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        IEnumerable<BraintreeTransactionViewModel> GetTransactionsViewModel(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        BraintreeTransactionViewModel GetTransactionViewModel(
            UmbracoContext umbracoContext,
            string transactionId);

        /// <summary>
        /// Gets the boot grid transactions.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchPhrase">The search phrase.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        BootGridViewModel<BraintreeTransactionViewModel> GetBootGridTransactions(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext);
    }
}
