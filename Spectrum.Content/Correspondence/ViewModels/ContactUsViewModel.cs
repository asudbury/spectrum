namespace Spectrum.Content.Correspondence.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ContactUsViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [Required(ErrorMessage = "Please enter an Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Message")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

    }
}
