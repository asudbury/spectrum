namespace Spectrum.Content.Payments.Managers
{
    using ViewModels;
    using System.Collections.Generic;
    using Umbraco.Web;

    public interface ITransactionsManager
    {
        /// <summary>
        /// Gets the transactions view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        IEnumerable<TransactionViewModel> GetTransactionsViewModel(UmbracoContext umbracoContext);
    }
}
