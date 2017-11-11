namespace Spectrum.Content.Appointments.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class UpdateAppointmentViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        public IEnumerable<string> Attendees { get; set; }
    }
}
