namespace Spectrum.Content.Registration.ViewModels
{
    using DataAnnotationsExtensions;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// The RegisterViewModel class.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterViewModel"/> class.
        /// </summary>
        public RegisterViewModel()
        {
            MemberType = "Member";
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DisplayName("Email address")]
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Remote("CheckEmailInUse", "Registration", ErrorMessage = "The email address has already been registered")]
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

        /// <summary>
        /// Gets or sets a value indicating whether [member type].
        /// </summary>
        public string MemberType { get; set; }
    }
}
