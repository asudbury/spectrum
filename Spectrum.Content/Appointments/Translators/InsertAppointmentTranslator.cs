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
            DateTime startTime = viewModel.StartTime.ToUniversalTime();
            DateTime now = DateTime.Now.ToUniversalTime();

            AppointmentModel model = new AppointmentModel
            {
                CreatedTime = now,
                LasteUpdatedTime = now,
                StartTime = startTime,
                Duration = viewModel.Duration,
                Description = viewModel.Description,
                Location = viewModel.Location,
                PaymentId = string.Empty,
                ServiceProviderId = viewModel.ServiceProviderId,
                Attendees = GetAttendees(viewModel.Attendees)
            };
            
            return model;
        }

        /// <summary>
        /// Gets the attendees.
        /// </summary>
        /// <param name="attendees">The attendees.</param>
        /// <returns></returns>
        private List<AppointmentAttendeeModel> GetAttendees(IEnumerable<string> attendees)
        {
            List<AppointmentAttendeeModel> models = new List<AppointmentAttendeeModel>();

            foreach (string attendee in attendees)
            {
                //// For now put name as Unknown
                models.Add(new AppointmentAttendeeModel { EmailAddress = attendee });
            }

            return models;
        }
    }
}