﻿namespace Spectrum.Content.Appointments.Translators
{
    using Models;
    using ViewModels;

    public interface IAppointmentTranslator
    {
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        AppointmentViewModel Translate(
            string paymentsPage,
            ClientAppointmentModel model);
        
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        AppointmentModel Translate(AppointmentViewModel viewModel);

        /// <summary>
        /// Translates the specified original model.
        /// </summary>
        /// <param name="originalModel">The original model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        AppointmentModel Translate(
            AppointmentModel originalModel,
            UpdateAppointmentViewModel viewModel);
    }
}
