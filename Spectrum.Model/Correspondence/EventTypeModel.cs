namespace Spectrum.Model.Correspondence
{
    using NPoco;

    [TableName("EventType")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class EventTypeModel
    {
        /// <summary>
        /// Gets the event.
        /// </summary>
        public Event Id { get; }

        /// <summary>
        /// Gets the event.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventModel" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The identifier description</param>
        public EventTypeModel(
            Event eventType,
            string description)
        {
            Id = eventType;
            Description = description;
        }
    }
}
