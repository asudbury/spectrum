namespace Spectrum.Content.Authentication.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ForgottenPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DisplayName("Email address")]
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }
    }
}
