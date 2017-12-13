namespace Spectrum.Content.Invoices.Translators
{
    using Content.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;

    public class InvoicesBootGridTranslator : BaseBootGridTranslator, IInvoicesBootGridTranslator
    {
        /// <summary>
        /// Translates the specified view models.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="sortItems">The sort items.</param>
        /// <returns></returns>
        public BootGridViewModel<InvoiceViewModel> Translate(
            List<InvoiceViewModel> viewModels, 
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

            List<InvoiceViewModel> rows = new List<InvoiceViewModel>();

            Tuple<int, int> range = GetRange(viewModels.Count, current, rowCount);

            if (range.Item1 != -1)
            {
                rows = viewModels.GetRange(range.Item1, range.Item2);
            }

            BootGridViewModel<InvoiceViewModel> bootGridViewModel = new BootGridViewModel<InvoiceViewModel>
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
        internal List<InvoiceViewModel> GetViewModels(
                    List<InvoiceViewModel> originalViewModels,
                    string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return originalViewModels;
            }

            List<InvoiceViewModel> viewModels = new List<InvoiceViewModel>();

            if (string.IsNullOrEmpty(searchString) == false)
            {
                foreach (InvoiceViewModel invoiceViewModel in originalViewModels)
                {
                    if (searchString.ToLower() == "unpaid" &&
                        string.IsNullOrEmpty(invoiceViewModel.PaymentId))
                    {
                        viewModels.Add(invoiceViewModel);
                    }
                    
                    else if (IsDateCheckKeywordSearch(
                             searchString,
                             invoiceViewModel.InvoiceDate))
                    {
                        viewModels.Add(invoiceViewModel);
                    }

                    else if (invoiceViewModel.Id.ToString().ToLower().Contains(searchString) ||
                             invoiceViewModel.InvoiceDate.ToString("ddd dd MMM").ToLower().Contains(searchString) ||
                             invoiceViewModel.ClientName.Contains(searchString) ||
                             invoiceViewModel.PaymentId.ToLower().Contains(searchString))
                    {
                        viewModels.Add(invoiceViewModel);
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
        internal IEnumerable<InvoiceViewModel> GetSortData(
            IEnumerable<InvoiceViewModel> viewModels,
            IEnumerable<SortData> sortItems)
        {
            IEnumerable<InvoiceViewModel> invoiceList = viewModels as InvoiceViewModel[] ?? viewModels.ToArray();

            if (sortItems != null)
            {
                SortData sortData = sortItems.First();

                IOrderedEnumerable<InvoiceViewModel> invoiceViewModels = null;

                switch (sortData.Field)
                {
                    case "id":
                        invoiceViewModels = IsSortOrderAscending(sortData.Type) ?
                            invoiceList.OrderBy(x => x.Id) :
                            invoiceList.OrderByDescending(x => x.Id);
                        break;

                    case "date":
                        invoiceViewModels = IsSortOrderAscending(sortData.Type) ?
                            invoiceList.OrderBy(x => x.InvoiceDate) :
                            invoiceList.OrderByDescending(x => x.InvoiceDate);
                        break;

                    /*case "client":
                        invoiceViewModels = IsSortOrderAscending(sortData.Type) ?
                            invoiceList.OrderBy(x => x.Status) :
                            invoiceList.OrderByDescending(x => x.Status);
                        break;*/

                    case "paymentId":
                        invoiceViewModels = IsSortOrderAscending(sortData.Type) ?
                            invoiceList.OrderBy(x => x.PaymentId) :
                            invoiceList.OrderByDescending(x => x.PaymentId);
                        break;
                }

                if (invoiceViewModels != null)
                {
                    invoiceList = invoiceViewModels.ToList();
                }
            }

            return invoiceList;
        }

    }
}
