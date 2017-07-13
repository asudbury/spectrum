namespace Spectrum.Content.Appointments.Models
{
    public enum AppointmentStatus
    {
        /// <summary>
        /// unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// outstanding.
        /// </summary>
        Outstanding = 1,

        /// <summary>
        /// completed
        /// </summary>
        Completed = 2,

        /// <summary>
        /// cancelled
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// deleted
        /// </summary>
        Deleted = 5
    }
}
