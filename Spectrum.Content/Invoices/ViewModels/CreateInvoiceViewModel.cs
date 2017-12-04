namespace Spectrum.Content.Invoices.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter a Invoice Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Client Name")]
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the invoice details.
        /// </summary>
        [Required(ErrorMessage = "Please enter Invoice Details")]
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the invoice amount.
        /// </summary>
        [Required(ErrorMessage = "Please enter Invoice Amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [Required(ErrorMessage = "Please enter Client Email Address")]
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
