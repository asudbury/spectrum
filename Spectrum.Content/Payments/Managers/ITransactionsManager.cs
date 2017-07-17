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
    }
}
