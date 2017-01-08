namespace Spectrum.Application.Appointments.Controllers
{
    using Providers;

    public class AppointmentsController
    {
        /// <summary>
        /// The appointments provider.
        /// </summary>
        private readonly IAppointmentsProvider appointmentsProvider;

        public AppointmentsController(IAppointmentsProvider appointmentsProvider)
        {
            this.appointmentsProvider = appointmentsProvider;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController"/> class.
        /// </summary>
        public AppointmentsController()
            :this(new AppointmentsProvider())
        {
        }
    }
}
