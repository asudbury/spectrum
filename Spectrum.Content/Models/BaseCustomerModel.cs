namespace Spectrum.Content.Models
{
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    public class BaseCustomerModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Notes { get; set; }
    }
}
