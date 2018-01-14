namespace Spectrum.Content.Customer.Translators
{
    using Content.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;

    public class CLientsBootGridTranslator : BaseBootGridTranslator, IClientsBootGridTranslator
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
        /// <inheritdoc />
        public BootGridViewModel<ClientViewModel> Translate(
            List<ClientViewModel> viewModels, 
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

            List<ClientViewModel> rows = new List<ClientViewModel>();

            Tuple<int, int> range = GetRange(viewModels.Count, current, rowCount);

            if (range.Item1 != -1)
            { 
                rows = viewModels.GetRange(range.Item1, range.Item2);
            }
            
            BootGridViewModel<ClientViewModel> bootGridViewModel = new BootGridViewModel<ClientViewModel>
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
        internal List<ClientViewModel> GetViewModels(
                    List<ClientViewModel> originalViewModels,
                    string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return originalViewModels;
            }

            List<ClientViewModel> viewModels = new List<ClientViewModel>();

            if (string.IsNullOrEmpty(searchString) == false)
            {
                foreach (ClientViewModel clientViewModel in originalViewModels)
                {
                    if (clientViewModel.Id.ToString().ToLower().Contains(searchString) ||
                        clientViewModel.Name.ToLower().Contains(searchString) ||
                        clientViewModel.EmailAddress.ToLower().Contains(searchString) ||
                        clientViewModel.HomePhoneNumber.ToLower().Contains(searchString) ||
                        clientViewModel.MobilePhoneNumber.ToLower().Contains(searchString) ||
                        clientViewModel.Address.ToLower().Contains(searchString) ||
                        clientViewModel.PostCode.ToLower().Contains(searchString) ||
                        clientViewModel.BuildingNumber.ToLower().Contains(searchString))
                    {
                        viewModels.Add(clientViewModel);
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
        internal IEnumerable<ClientViewModel> GetSortData(
            IEnumerable<ClientViewModel> viewModels,
            IEnumerable<SortData> sortItems)
        {
            IEnumerable<ClientViewModel> clientList = viewModels as ClientViewModel[] ?? viewModels.ToArray();

            if (sortItems != null)
            {
                SortData sortData = sortItems.First();
                IOrderedEnumerable<ClientViewModel> clientViewModels = null;
                
                switch (sortData.Field)
                {
                    case "id":
                        clientViewModels = IsSortOrderAscending(sortData.Type) ?
                            clientList.OrderBy(x => x.Id) :
                            clientList.OrderByDescending(x => x.Id);
                        break;

                    case "name":
                        clientViewModels = IsSortOrderAscending(sortData.Type) ?
                            clientList.OrderBy(x => x.Name) :
                            clientList.OrderByDescending(x => x.Name);
                        break;

                    case "email":
                        clientViewModels = IsSortOrderAscending(sortData.Type) ?
                            clientList.OrderBy(x => x.EmailAddress) :
                            clientList.OrderByDescending(x => x.EmailAddress);
                        break;

                    case "homeNo":
                        clientViewModels = IsSortOrderAscending(sortData.Type) ?
                            clientList.OrderBy(x => x.HomePhoneNumber) :
                            clientList.OrderByDescending(x => x.HomePhoneNumber);
                        break;

                    case "mobileNo":
                        clientViewModels = IsSortOrderAscending(sortData.Type) ?
                            clientList.OrderBy(x => x.MobilePhoneNumber) :
                            clientList.OrderByDescending(x => x.MobilePhoneNumber);
                        break;

                    case "address":
                        clientViewModels = IsSortOrderAscending(sortData.Type) ?
                            clientList.OrderBy(x => x.Address) :
                            clientList.OrderByDescending(x => x.Address);
                        break;
                }

                if (clientViewModels != null)
                {
                    clientList = clientViewModels.ToList();
                }
            }

            return clientList;
        }
    }
}