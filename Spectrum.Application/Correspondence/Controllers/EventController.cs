namespace Spectrum.Application.Correspondence.Controllers
{
    using Model.Correspondence;
    using Providers;

    public class EventController
    {
        /// <summary>
        /// The event provider.
        /// </summary>
        protected readonly IEventProvider eventProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventController" /> class.
        /// </summary>
        /// <param name="emailProvider">The email provider.</param>
        public EventController(IEventProvider eventProvider)
        {
            this.eventProvider = eventProvider;
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertEvent(EventModel model)
        {
            eventProvider.InsertEvent(model);
        }
    }
}
