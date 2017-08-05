using System.Globalization;

namespace Spectrum.Content.Payments.Translators
{
    using System;
    using System.Collections.Generic;
    using ViewModels;

    public class TransactionsBootGridTranslator : BaseBootGridTranslator, ITransactionsBootGridTranslator
    {
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
                foreach (TransactionViewModel appointmentViewModel in originalViewModels)
                {
                    if (appointmentViewModel.Id.ToLower().Contains(searchString) ||
                        appointmentViewModel.TransactionDateTime.Value.ToString("ddd dd MMM HH:mm").ToLower().Contains(searchString) ||
                        appointmentViewModel.Type.ToLower().Contains(searchString) ||
                        appointmentViewModel.Status.ToLower().Contains(searchString) ||
                        appointmentViewModel.CardType.ToLower().Contains(searchString) ||
                        appointmentViewModel.MaskedNumber.ToLower().Contains(searchString) ||
                        appointmentViewModel.Amount.Value.ToString("F").Contains(searchString))
                    {
                        viewModels.Add(appointmentViewModel);
                    }
                }
            }

            return viewModels;
        }

    }
}