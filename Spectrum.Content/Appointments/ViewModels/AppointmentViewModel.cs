using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Spectrum.Content.Appointments.ViewModels
{
    using System;

    public class AppointmentViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the encrypted identifier.
        /// </summary>
        public string EncryptedId { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the take payment URL.
        /// </summary>
        public string TakePaymentUrl { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        public IEnumerable<string> Attendees { get; set; }
    }
}
