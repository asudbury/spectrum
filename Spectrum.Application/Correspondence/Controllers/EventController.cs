namespace Spectrum.Application.Correspondence.Controllers
{
    using Model.Correspondence;
    using Providers;

    public class EventController
    {
        /// <summary>
        /// The event provider.
        /// </summary>
        protected readonly IEventProvider EventProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventController" /> class.
        /// </summary>
        /// <param name="eventProvider">The event provider.</param>
        public EventController(IEventProvider eventProvider)
        {
            EventProvider = eventProvider;
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertEvent(EventModel model)
        {
            EventProvider.InsertEvent(model);
        }
    }
}
