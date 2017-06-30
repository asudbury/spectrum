namespace Spectrum.Content.Payments.Managers
{
    using ViewModels;
    using Umbraco.Web;

    public interface ITransactionsManager
    {
        /// <summary>
        /// Gets the transactions view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        TransactionsViewModel GetTransactionsViewModel(UmbracoContext umbracoContext);
    }
}
