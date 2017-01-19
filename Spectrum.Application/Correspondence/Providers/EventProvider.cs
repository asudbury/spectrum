
namespace Spectrum.Application.Correspondence.Providers
{
    using Model.Correspondence;
    using Repositories;

    /// <summary>
    /// The Event Provider class.
    /// </summary>
    internal class EventProvider : IEventProvider
    {
        /// <summary>
        /// The event repository.
        /// </summary>
        protected readonly IEventRepository eventRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProvider" /> class.
        /// </summary>
        /// <param name="emailRepository">The event repository.</param>
        internal EventProvider(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProvider"/> class.
        /// </summary>
        internal EventProvider()
            : this(new EventRepository())
        {
        }

        /// <summary>
        /// Record an event.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertEvent(EventModel model)
        {
            this.eventRepository.InsertEvent(model);
        }
    }
}
