using Spectrum.Content.Appointments.Models;
using Spectrum.Content.Models;

namespace Spectrum.Content.Payments.Managers
{
    using System.Collections.Generic;
    using Umbraco.Web;
    using ViewModels;

    public interface ITransactionsManager
    {
        /// <summary>
        /// Gets the transactions view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        IEnumerable<TransactionViewModel> GetTransactionsViewModel(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        TransactionViewModel GetTransactionViewModel(
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
        BootGridViewModel<TransactionViewModel> GetBootGridTransactions(
            int current,
            int rowCount,
            string searchPhrase,
            IEnumerable<SortData> sortItems,
            UmbracoContext umbracoContext);
    }
}
