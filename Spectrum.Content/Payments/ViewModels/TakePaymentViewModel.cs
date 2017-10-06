namespace Spectrum.Content.Payments.ViewModels
{
    public class TakePaymentViewModel
    {
        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        public string NodeId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the appointment identifier.
        /// </summary>
        public string AppointmentId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
