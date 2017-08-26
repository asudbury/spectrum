namespace Spectrum.Content.Appointments.Translators
{
    using Models;
    using System;
    using ViewModels;

    public class InsertAppointmentTranslator : IInsertAppointmentTranslator
    {
        /// <inheritdoc />
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public AppointmentModel Translate(InsertAppointmentViewModel viewModel)
        {
            AppointmentModel model = new AppointmentModel
            {
                CreatedTime = DateTime.Now,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                Description = viewModel.Description,
                Location = viewModel.Location,
                PaymentId = string.Empty,
                Status = (int)AppointmentStatus.Outstanding
            };
            
            return model;
        }
    }
}