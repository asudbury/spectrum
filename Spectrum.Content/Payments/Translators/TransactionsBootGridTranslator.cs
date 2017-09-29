namespace Spectrum.Content.Payments.Translators
{
    using System;
    using System.Collections.Generic;
    using ViewModels;

    public class TransactionsBootGridTranslator : BaseBootGridTranslator, ITransactionsBootGridTranslator
    {
        /// <inheritdoc />
        /// <summary>
        /// Translates the specified view models.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        public BootGridViewModel<TransactionViewModel> Translate(
            List<TransactionViewModel> viewModels, 
            int current, 
            int rowCount, 
            string searchString)
        {
            viewModels = GetViewModels(viewModels, searchString.ToLower());

            List<TransactionViewModel> rows = new List<TransactionViewModel>();

            Tuple<int, int> range = GetRange(viewModels.Count, current, rowCount);

            if (range.Item1 != -1)
            {
                rows = viewModels.GetRange(range.Item1, range.Item2);
            }

            BootGridViewModel<TransactionViewModel> bootGridViewModel = new BootGridViewModel<TransactionViewModel>
            {
                Rows = rows,
                Current = current,
                RowCount = rowCount,
                Total = viewModels.Count

            };

            return bootGridViewModel;
        }

        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="originalViewModels">The original view models.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        public List<TransactionViewModel> GetViewModels(
            List<TransactionViewModel> originalViewModels,
            string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return originalViewModels;
            }

            List<TransactionViewModel> viewModels = new List<TransactionViewModel>();

            if (string.IsNullOrEmpty(searchString) == false)
            {
                foreach (TransactionViewModel transactionViewModel in originalViewModels)
                {
                    if (IsDateCheckKeywordSearch(
                        searchString, 
                        transactionViewModel.TransactionDateTime.Value))
                    {
                        viewModels.Add(transactionViewModel);
                    }

                    else if (transactionViewModel.Id.ToLower().Contains(searchString) ||
                             transactionViewModel.TransactionDateTime.Value.ToString("ddd dd MMM HH:mm").ToLower().Contains(searchString) ||
                             transactionViewModel.Type.ToLower().Contains(searchString) ||
                             transactionViewModel.Status.ToLower().Contains(searchString) ||
                             transactionViewModel.CardType.ToLower().Contains(searchString) ||
                             transactionViewModel.MaskedNumber.ToLower().Contains(searchString) ||
                             transactionViewModel.Amount.Value.ToString("F").Contains(searchString))
                    {
                        viewModels.Add(transactionViewModel);
                    }
                }
            }

            return viewModels;
        }

    }
}