namespace Spectrum.Content.Appointments.Translators
{
    using Models;
    using System;
    using System.Collections.Generic;
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
                CreatedTime = DateTime.Now.ToUniversalTime(),
                StartTime = viewModel.StartTime.ToUniversalTime(),
                EndTime = viewModel.EndTime.ToUniversalTime(),
                Description = viewModel.Description,
                Location = viewModel.Location,
                PaymentId = string.Empty,
                Status = (int)AppointmentStatus.Outstanding,
                Attendees = GetAttendees(viewModel.Attendees)
            };
            
            return model;
        }

        /// <summary>
        /// Gets the attendees.
        /// </summary>
        /// <param name="attendees">The attendees.</param>
        /// <returns></returns>
        private List<AppointmentAttendeeModel> GetAttendees(List<string> attendees)
        {
            List<AppointmentAttendeeModel> models = new List<AppointmentAttendeeModel>();

            foreach (string attendee in attendees)
            {
                //// For now put name as Unknown
                models.Add(new AppointmentAttendeeModel { Name = "Unknown", EmailAddress = attendee });
            }

            return models;
        }
    }
}