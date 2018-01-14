﻿namespace Spectrum.Content.Appointments.ViewModels
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
        /// Gets or sets the encrypted customer identifier.
        /// </summary>
        public string EncryptedCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the lasted updated user.
        /// </summary>
        public string LastedUpdatedUser { get; set; }

        /// <summary>
        /// Gets or sets the last updated time.
        /// </summary>
        public DateTime LastUpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

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
        /// Gets or sets the invoice identifier.
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the view appointment URL.
        /// </summary>
        public string ViewAppointmentUrl { get; set; }

        /// <summary>
        /// Gets or sets the update appointment URL.
        /// </summary>
        public string UpdateAppointmentUrl { get; set; }

        /// <summary>
        /// Gets or sets the delete appointment URL.
        /// </summary>
        public string DeleteAppointmentUrl { get; set; }

        /// <summary>
        /// Gets or sets the google search URL.
        /// </summary>
        public string GoogleSearchUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        public string ClientUrl { get; set; }
        
    }
}
