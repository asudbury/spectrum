namespace Spectrum.Content.Payments.Factories
{
    using Umbraco.Web;

    public interface IPaymentProviderFactory
    {
        /// <summary>
        /// Gets the transactions partial view.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="useSpectrumPage">if set to <c>true</c> [use spectrum page].</param>
        /// <returns></returns>
        string GetTransactionsPartialView(
            UmbracoContext umbracoContext,
            bool useSpectrumPage);

        /// <summary>
        /// Gets the transaction partial view.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="useSpectrumPage">if set to <c>true</c> [use spectrum page].</param>
        /// <returns></returns>
        string GetTransactionPartialView(
            UmbracoContext umbracoContext,
            bool useSpectrumPage);

        /// <summary>
        /// Gets the payment partial view.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        string GetPaymentPartialView(
            UmbracoContext umbracoContext,
            int?  customerId = null);
    }
}
