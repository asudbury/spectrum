namespace Spectrum.Content.Payments.Translators
{
    using System.Collections.Generic;
    using ViewModels;

    public interface ITransactionsBootGridTranslator
    {
        /// <summary>
        /// Translates the specified view models.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        BootGridViewModel<TransactionViewModel> Translate(
            List<TransactionViewModel> viewModels,
            int current,
            int rowCount,
            string searchString);
    }
}
