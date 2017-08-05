﻿namespace Spectrum.Content.Appointments.Translators
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
            Tuple<int, int> range = GetRange(viewModels.Count, current, rowCount);

            //// now just get the data required!
            List<AppointmentViewModel> rows = viewModels.GetRange(range.Item1, range.Item2);

            BootGridViewModel<AppointmentViewModel> bootGridViewModel = new BootGridViewModel<AppointmentViewModel>
            {
                Rows = rows,
                Current = current,
                RowCount = rowCount,
                Total = viewModels.Count

            };

            return bootGridViewModel;
        }
    }
}