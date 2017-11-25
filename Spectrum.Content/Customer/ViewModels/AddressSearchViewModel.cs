namespace Spectrum.Content.Customer.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddressSearchViewModel
    {
        /// <summary>
        /// Gets or sets the building number.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Building Number")]
        public string BuildingNumber { get; set; }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        [Required(ErrorMessage = "Please enter a Post Code")]
        public string PostCode { get; set; }
    }
}
