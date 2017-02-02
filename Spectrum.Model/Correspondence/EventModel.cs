namespace Spectrum.Model.Correspondence
{
    using NPoco;
    using System;

    /// <summary>
    /// The Event Model
    /// </summary>
    [TableName("Event")]
    public class EventModel 
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Gets the event.
        /// </summary>
        public Event Event { get; }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Gets Description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets Text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventModel" /> class.
        /// </summary>
        /// <param name="guid">The identifier.</param>
        /// <param name="userEvent">The user event.</param>
        public EventModel(
            Guid guid,
            Event userEvent)
            : this(guid, userEvent, "", "", DateTime.Now)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventModel" /> class.
        /// </summary>
        /// <param name="guid">The identifier.</param>
        /// <param name="userEvent">The user event.</param>
        /// <param name="description">The event description.</param>
        /// <param name="text">The event text.</param>
        public EventModel(
            Guid guid,
            Event userEvent,
            String description,
            String text)
            : this(guid, userEvent, description, text, DateTime.Now)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventModel" /> class.
        /// </summary>
        /// <param name="guid">The identifier.</param>
        /// <param name="userEvent">The user event.</param>
        /// <param name="dateTime">The date time.</param>
        public EventModel(
            Guid guid,
            Event userEvent,
            String description,
            String text,
            DateTime dateTime)
        {
            Guid = guid;
            Event = userEvent;
            Description = description;
            Text = text;
            DateTime = dateTime;
        }
    }
}
