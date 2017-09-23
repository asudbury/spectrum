namespace Spectrum.Content.Appointments.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InsertAppointmentViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertAppointmentViewModel"/> class.
        /// </summary>
        public InsertAppointmentViewModel()
        {
            //// we need to set some default dates
            //// other wise we get the DateTime default date
            StartTime = DateTime.Now;
            Attendees = new List<string>();
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Start Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dddd dd-MMM-yyyy HH:mm}")]
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Duration")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        public List<string> Attendees { get; set; }
    }
}
