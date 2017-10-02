namespace Spectrum.Content.Appointments.Translators
{
    using Content.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using ViewModels;

    public class AppointmentsBootGridTranslator : BaseBootGridTranslator, IAppointmentsBootGridTranslator
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
        public BootGridViewModel<AppointmentViewModel> Translate(
            List<AppointmentViewModel> viewModels, 
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

            List<AppointmentViewModel> rows = new List<AppointmentViewModel>();

            Tuple<int, int> range = GetRange(viewModels.Count, current, rowCount);

            if (range.Item1 != -1)
            { 
                rows = viewModels.GetRange(range.Item1, range.Item2);
            }
            
            BootGridViewModel<AppointmentViewModel> bootGridViewModel = new BootGridViewModel<AppointmentViewModel>
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
        public List<AppointmentViewModel> GetViewModels(
                    List<AppointmentViewModel> originalViewModels,
                    string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return originalViewModels;
            }

            List<AppointmentViewModel> viewModels = new List<AppointmentViewModel>();

            if (string.IsNullOrEmpty(searchString) == false)
            {
                foreach (AppointmentViewModel appointmentViewModel in originalViewModels)
                {
                    if (searchString.ToLower() == "unpaid" &&
                        string.IsNullOrEmpty(appointmentViewModel.PaymentId))
                    {
                        viewModels.Add(appointmentViewModel);
                    }

                    else if (IsDateCheckKeywordSearch(
                             searchString, 
                             appointmentViewModel.StartTime))
                    { 
                        viewModels.Add(appointmentViewModel);
                    }

                    else if (appointmentViewModel.Id.ToString().ToLower().Contains(searchString) ||
                        appointmentViewModel.StartTime.ToString("ddd dd MMM HH:mm").ToLower().Contains(searchString) ||
                        appointmentViewModel.Duration.ToString(CultureInfo.InvariantCulture).Contains(searchString) ||
                        appointmentViewModel.Status.ToLower().Contains(searchString) ||
                        appointmentViewModel.PaymentId.ToLower().Contains(searchString) ||
                        appointmentViewModel.Location.ToLower().Contains(searchString) ||
                        appointmentViewModel.Description.ToLower().Contains(searchString))
                    {
                        viewModels.Add(appointmentViewModel);
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
        public IEnumerable<AppointmentViewModel> GetSortData(
            IEnumerable<AppointmentViewModel> viewModels,
            IEnumerable<SortData> sortItems)
        {
            IEnumerable<AppointmentViewModel> appointmentList = viewModels as AppointmentViewModel[] ?? viewModels.ToArray();

            if (sortItems != null)
            {
                SortData sortData = sortItems.First();
                IOrderedEnumerable<AppointmentViewModel> appointmentViewModels = null;
                
                switch (sortData.Field)
                {
                    case "id":
                        appointmentViewModels = IsSortOrderAscending(sortData.Type) ?
                            appointmentList.OrderBy(x => x.Id) :
                            appointmentList.OrderByDescending(x => x.Id);
                        break;

                    case "startTime":
                        appointmentViewModels = IsSortOrderAscending(sortData.Type) ?
                            appointmentList.OrderBy(x => x.StartTime) :
                            appointmentList.OrderByDescending(x => x.StartTime);
                        break;

                    case "status":
                        appointmentViewModels = IsSortOrderAscending(sortData.Type) ?
                            appointmentList.OrderBy(x => x.Status) :
                            appointmentList.OrderByDescending(x => x.Status);
                        break;

                    case "paymentId":
                        appointmentViewModels = IsSortOrderAscending(sortData.Type) ?
                            appointmentList.OrderBy(x => x.PaymentId) :
                            appointmentList.OrderByDescending(x => x.PaymentId);
                        break;

                    case "location":
                        appointmentViewModels = IsSortOrderAscending(sortData.Type) ?
                            appointmentList.OrderBy(x => x.Location) :
                            appointmentList.OrderByDescending(x => x.Location);
                        break;

                    case "description":
                        appointmentViewModels = IsSortOrderAscending(sortData.Type) ?
                            appointmentList.OrderBy(x => x.Description) :
                            appointmentList.OrderByDescending(x => x.Description);
                        break;
                }

                if (appointmentViewModels != null)
                {
                    appointmentList = appointmentViewModels.ToList();
                }
            }

            return appointmentList;
        }
    }
}