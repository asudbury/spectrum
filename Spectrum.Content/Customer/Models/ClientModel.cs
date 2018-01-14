namespace Spectrum.Content.Customer.Models
{
    using Content.Models;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    [TableName(Constants.Database.ClientTableName)]
    [PrimaryKey("Id", autoIncrement = true)]
    public class ClientModel : BaseCustomerModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1000)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the home phone number.
        /// </summary>
        public string HomePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the moble phone number.
        /// </summary>
        public string MobilePhoneNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the building number.
        /// </summary>
        public string BuildingNumber { get; set; }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }
    }
}
