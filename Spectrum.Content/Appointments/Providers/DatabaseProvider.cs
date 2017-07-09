namespace Spectrum.Content.Appointments.Providers
{
    using Models;
    using Umbraco.Core;

    public class DatabaseProvider : IDatabaseProvider
    {
        /// <summary>
        /// Inserts the appointment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The appointmentId</returns>
        public string InsertAppointment(AppointmentModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            object appointmentId = context.Database.Insert(model);

            return appointmentId.ToString();
        }
    }
}
