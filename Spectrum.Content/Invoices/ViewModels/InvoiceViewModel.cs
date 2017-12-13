namespace Spectrum.Content.Invoices.ViewModels
{
    using System;

    public class InvoiceViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        public DateTime InvoiceDate { get; set; }

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
        /// Gets or sets the name of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the invoice details.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the invoice amount.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the view invoice URL.
        /// </summary>
        public string ViewInvoiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the update invoice URL.
        /// </summary>
        public string UpdateInvoiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the delete invoice URL.
        /// </summary>
        public string DeleteInvoiceUrl { get; set; }
    }
}
