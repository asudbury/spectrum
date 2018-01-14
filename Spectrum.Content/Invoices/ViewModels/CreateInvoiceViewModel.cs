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
            EmailClientInvoice = true;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Invoice Date")]
        public DateTime Date { get; set; }

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
        /// Gets or sets a value indicating whether [email client invoice].
        /// </summary>
        [DisplayName("Email client copy of invoice")]
        public bool EmailClientInvoice { get; set; }
    }
}
