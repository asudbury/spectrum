namespace Spectrum.Content.Appointments.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class EventViewModel
    {
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        public IList<string> Attendees { get; set; }
    }
}
