namespace Spectrum.Content.Appointments.Models
{
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    [TableName(Constants.Database.AppointmentStatusTableName)]
    [PrimaryKey("Id")]
    public class AppointmentStatusModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column("Id")]
        [PrimaryKeyColumn]
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
