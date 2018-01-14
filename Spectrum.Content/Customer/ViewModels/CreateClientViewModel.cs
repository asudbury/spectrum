namespace Spectrum.Content.Customer.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateClientViewModel : AddressSearchViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [Required(ErrorMessage = "Please enter an Email address")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the home phone number.
        /// </summary>
        [RegularExpression("^\\(?[01](\\s*\\d\\)?){9,16}$", ErrorMessage = "Please enter a valid Phone number")]
        public string HomePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone number.
        /// </summary>
        [RegularExpression("^07\\d{3}\\s{0,1}\\d{6}$", ErrorMessage = "Please enter a valid Mobile Phone number")]
        public string MobilePhoneNumber { get; set; }
    }
}
