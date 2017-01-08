namespace Spectrum.Content.Authentication.ViewModels
{
    using DataAnnotationsExtensions;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DisplayName("Email address")]
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [UIHint("Password")]
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [UIHint("Password")]
        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [EqualTo("Password", ErrorMessage = "Youyr passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
