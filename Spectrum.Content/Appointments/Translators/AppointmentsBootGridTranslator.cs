namespace Spectrum.Content.Appointments.Translators
{
    using System;
    using System.Collections.Generic;
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
        /// <returns></returns>
        public BootGridViewModel<AppointmentViewModel> Translate(
            List<AppointmentViewModel> viewModels, 
            int current, 
            int rowCount, 
            string searchString)
        {
            viewModels = GetViewModels(viewModels, searchString.ToLower());

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
                    if (appointmentViewModel.Id.ToString().ToLower().Contains(searchString) ||
                        appointmentViewModel.StartTime.ToString("ddd dd MMM HH:mm").ToLower().Contains(searchString) ||
                        appointmentViewModel.EndTime.ToString("ddd dd MMM HH:mm").ToLower().Contains(searchString) ||
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
    }
}