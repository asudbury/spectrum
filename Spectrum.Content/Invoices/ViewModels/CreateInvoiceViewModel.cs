namespace Spectrum.Content.Invoices.ViewModels
{
    using System;
    using System.ComponentModel;

    public class CreateInvoiceViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvoiceViewModel"/> class.
        /// </summary>
        public CreateInvoiceViewModel()
        {
            Date = DateTime.Today;
            IncludePaymentLink = true;
        }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        public DateTime Date { get; set; }

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
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show include payment link].
        /// </summary>
        public bool ShowIncludePaymentLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include payment link].
        /// </summary>
        [DisplayName("Include link for payment")]
        public bool IncludePaymentLink { get; set; }
    }
}
