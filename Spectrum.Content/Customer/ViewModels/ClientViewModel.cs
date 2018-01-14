namespace Spectrum.Content.Customer.ViewModels
{
    using System;

    public class ClientViewModel : AddressSearchViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the google search URL.
        /// </summary>
        public string GoogleSearchUrl { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the home phone number.
        /// </summary>
        public string HomePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone number.
        /// </summary>
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the last updated time.
        /// </summary>

        public DateTime LastUpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets the last updated user.
        /// </summary>
        public string LastUpdatedUser { get; set; }

        /// <summary>
        /// Gets or sets the create quote URL.
        /// </summary>
        public string CreateQuoteUrl { get; set; }

        /// <summary>
        /// Gets or sets the create invoice URL.
        /// </summary>
        public string CreateInvoiceUrl { get; set; }
        
        /// <summary>
        /// Gets or sets the create appointment URL.
        /// </summary>
        public string CreateAppointmentUrl { get; set; }

        /// <summary>
        /// Gets or sets the view client URL.
        /// </summary>
        public string ViewClientUrl { get; set; }

        /// <summary>
        /// Gets or sets the update client URL.
        /// </summary>
        public string UpdateClientUrl { get; set; }
    }
}
