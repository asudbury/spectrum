namespace Spectrum.Content.Correspondence.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ForumPostViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Message")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
