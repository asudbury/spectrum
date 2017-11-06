namespace Spectrum.Content.Appointments.Translators
{
    using Application.Services;
    using Models;
    using Payments;
    using System;
    using System.Collections.Generic;
    using ViewModels;

    public class AppointmentTranslator : IAppointmentTranslator
    {
        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentTranslator"/> class.
        /// </summary>
        /// <param name="encryptionService">The encryption service.</param>
        public AppointmentTranslator(IEncryptionService encryptionService)
        {
            this.encryptionService = encryptionService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AppointmentViewModel Translate(
            string paymentsPage,
            AppointmentModel model)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(model.StartTime);

            AppointmentViewModel viewModel = new AppointmentViewModel
            {
                Id = model.Id,
                EncryptedId = encryptionService.EncryptString(model.Id.ToString()),
                CreatedTime = TimeZone.CurrentTimeZone.ToLocalTime(model.CreatedTime),
                CreatedUser = model.CreatedUser,
                LastUpdatedTime = TimeZone.CurrentTimeZone.ToLocalTime(model.LasteUpdatedTime),
                LastedUpdatedUser = model.LastedUpdatedUser,
                StartTime = startTime,
                Duration = model.Duration,
                Status = ((AppointmentStatus)model.Status).ToString(),
                PaymentId = model.PaymentId,
                Location = model.Location,
                Description = model.Description,
                ViewAppointmentUrl = BuildAppointmentUrl(paymentsPage, model.Id, "viewappointment"),
                UpdateAppointmentUrl = BuildAppointmentUrl(paymentsPage, model.Id, "updateappointment"),
                DeleteAppointmentUrl = "/umbraco/Surface/Appointments/Delete/" + encryptionService.EncryptString(model.Id.ToString()),
                TakePaymentUrl = BuildPaymentsUrl(paymentsPage, model.Id),
                Attendees = GetAttendees(model.Attendees)
            };

            return viewModel;
        }

        /// <inheritdoc />
        /// <summary>
        /// Translates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public AppointmentModel Translate(AppointmentViewModel viewModel)
        {
            AppointmentModel model = new AppointmentModel
            {
                Id = viewModel.Id,
                CreatedTime = viewModel.CreatedTime,
                CreatedUser = viewModel.CreatedUser,
                LasteUpdatedTime = viewModel.LastUpdatedTime,
                LastedUpdatedUser = viewModel.LastedUpdatedUser,
                StartTime = viewModel.StartTime,
                Duration = viewModel.Duration,
                Location = viewModel.Location,
                Description = viewModel.Description,
                PaymentId = viewModel.PaymentId
            };

            return model;
        }

        /// <summary>
        /// Translates the specified original model.
        /// </summary>
        /// <param name="originalModel">The original model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public AppointmentModel Translate(
            AppointmentModel originalModel,
            AppointmentViewModel viewModel)
        {
            DateTime now = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(viewModel.StartTime);
            
            AppointmentModel model = new AppointmentModel
            {
                Id = originalModel.Id,
                CustomerId = originalModel.CustomerId,
                ServiceProviderId = originalModel.ServiceProviderId,
                CreatedTime = originalModel.CreatedTime,
                CreatedUser = originalModel.CreatedUser,
                LasteUpdatedTime = now,
                StartTime = startTime,
                Duration = viewModel.Duration,
                Location = viewModel.Location,
                Description = viewModel.Description,
                PaymentId = viewModel.PaymentId
            };

            return model;
        }

        /// <summary>
        /// Builds the appointment URL.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        internal string BuildAppointmentUrl(
            string paymentsPage,
            int appointmentId,
            string page)
        {
            string url = string.Empty;

            if (string.IsNullOrEmpty(paymentsPage) == false)
            {
                //// TODO : this is rubbish but will do for now!
                url = paymentsPage.Replace("payment", page) + "?" + PaymentsQueryStringConstants.AppointmentId + "=" + encryptionService.EncryptString(appointmentId.ToString());
            }

            return url;
        }

        /// <summary>
        /// Builds the payments URL.
        /// </summary>
        /// <param name="paymentsPage">The payments page.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        internal string BuildPaymentsUrl(
            string paymentsPage,
            int appointmentId)
        {
            string url = string.Empty;

            if (string.IsNullOrEmpty(paymentsPage) == false)
            {
                url = paymentsPage + "?" + PaymentsQueryStringConstants.AppointmentId + "=" + encryptionService.EncryptString(appointmentId.ToString());
            }

            return url;
        }

        /// <summary>
        /// Gets the attendees.
        /// </summary>
        /// <param name="attendeeModels">The attendee models.</param>
        /// <returns></returns>
        internal IEnumerable<string> GetAttendees(IEnumerable<AppointmentAttendeeModel> attendeeModels)
        {
            List<string> attendees = new List<string>();

            if (attendeeModels != null)
            {
                foreach (AppointmentAttendeeModel attendeeModel in attendeeModels)
                {
                    attendees.Add(attendeeModel.EmailAddress);
                }
            }

            return attendees;
        }
    }
}