namespace Spectrum.Content.Payments.Translators
{
    using Content.Models;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;

    public class TransactionsBootGridTranslator : BaseBootGridTranslator, ITransactionsBootGridTranslator
    {
        private IOrderedEnumerable<BraintreeTransactionViewModel> transactionViewModels;

        /// <summary>
        /// Translates the specified view models.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public BootGridViewModel<BraintreeTransactionViewModel> Translate(
            List<BraintreeTransactionViewModel> viewModels, 
            int current, 
            int rowCount, 
            string searchString,
            IEnumerable<SortData> sortItems)
        {
            viewModels = GetViewModels(viewModels, searchString.ToLower());

            //// now do the order by!

            if (sortItems != null)
            {
                viewModels = GetSortData(viewModels, sortItems).ToList();
            }

            List<BraintreeTransactionViewModel> rows = new List<BraintreeTransactionViewModel>();

            Tuple<int, int> range = GetRange(viewModels.Count, current, rowCount);

            if (range.Item1 != -1)
            {
                rows = viewModels.GetRange(range.Item1, range.Item2);
            }

            BootGridViewModel<BraintreeTransactionViewModel> bootGridViewModel = new BootGridViewModel<BraintreeTransactionViewModel>
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
        public List<BraintreeTransactionViewModel> GetViewModels(
            List<BraintreeTransactionViewModel> originalViewModels,
            string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return originalViewModels;
            }

            List<BraintreeTransactionViewModel> viewModels = new List<BraintreeTransactionViewModel>();

            if (string.IsNullOrEmpty(searchString) == false)
            {
                foreach (BraintreeTransactionViewModel transactionViewModel in originalViewModels)
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

        /// <summary>
        /// Gets the sort data.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <returns></returns>
        public IEnumerable<BraintreeTransactionViewModel> GetSortData(
            IEnumerable<BraintreeTransactionViewModel> viewModels,
            IEnumerable<SortData> sortItems)
        {
            IEnumerable<BraintreeTransactionViewModel> transactionsList = viewModels as BraintreeTransactionViewModel[] ?? viewModels.ToArray();

            if (sortItems != null)
            {
                SortData sortData = sortItems.First();
                transactionViewModels = null;

                switch (sortData.Field)
                {
                    case "id":
                        transactionViewModels = IsSortOrderAscending(sortData.Type) ? 
                            transactionsList.OrderBy(x => x.Id) : 
                            transactionsList.OrderByDescending(x => x.Id);
                        break;

                    case "transactionDateTime":
                        transactionViewModels = IsSortOrderAscending(sortData.Type) ?
                            transactionsList.OrderBy(x => x.TransactionDateTime) :
                            transactionsList.OrderByDescending(x => x.TransactionDateTime);
                        break;

                    case "status":
                        transactionViewModels = IsSortOrderAscending(sortData.Type) ?
                            transactionsList.OrderBy(x => x.Status) :
                            transactionsList.OrderByDescending(x => x.Status);
                        break;

                    case "paymentInformation":
                        transactionViewModels = IsSortOrderAscending(sortData.Type) ?
                            transactionsList.OrderBy(x => x.MaskedNumber) :
                            transactionsList.OrderByDescending(x => x.MaskedNumber);
                        break;

                    case "amount":
                        transactionViewModels = IsSortOrderAscending(sortData.Type) ?
                            transactionsList.OrderBy(x => x.Amount) :
                            transactionsList.OrderByDescending(x => x.Amount);
                        break;
                }

                if (transactionViewModels != null)
                {
                    transactionsList = transactionViewModels.ToList();
                }
            }

            return transactionsList;
        }
    }
}