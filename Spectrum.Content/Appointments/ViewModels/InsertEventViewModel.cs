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
            EndTime = DateTime.Now.AddHours(1);    
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Required(ErrorMessage = "Please enter a start time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dddd dd-MMM-yyyy HH:mm}")]
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// Gets or sets the end date time.
        /// </summary>
        [Required(ErrorMessage = "Please enter an end time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dddd dd-MMM-yyyy HH:mm}")]
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [Required(ErrorMessage = "Please enter a location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required(ErrorMessage = "Please enter a description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        public IList<string> Attendees { get; set; }
    }
}
