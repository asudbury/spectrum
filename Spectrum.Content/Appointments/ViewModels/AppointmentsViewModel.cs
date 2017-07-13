namespace Spectrum.Content.Appointments.ViewModels
{
    using System.Collections.Generic;

    public class AppointmentsViewModel
    {
        /// <summary>
        /// Gets the appointments.
        /// </summary>
        public IEnumerable<AppointmentViewModel> Appointments { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsViewModel" /> class.
        /// </summary>
        /// <param name="appointments">The appointments.</param>
        public AppointmentsViewModel(IEnumerable<AppointmentViewModel> appointments)
        {
            Appointments = appointments;
        }
    }
}
