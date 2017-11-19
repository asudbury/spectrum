namespace Spectrum.Content.Payments.ViewModels
{
    public class PaymentContextViewModel
    {
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the payment provider.
        /// </summary>
        public string PaymentProvider { get; set; }

        /// <summary>
        /// Gets or sets the automatic allocate.
        /// </summary>
        public string AutoAllocate { get; set; }

        /// <summary>
        /// Gets or sets the appointment identifier.
        /// </summary>
        public string AppointmentId { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the payment amount.
        /// </summary>
        public string PaymentAmount { get; set; }

        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        public string NodeId { get; set; }

        /// <summary>
        /// Gets or sets the make payment URL.
        /// </summary>
        public string MakePaymentUrl { get; set; }

        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        public string AuthToken { get; set; }
    }
}
