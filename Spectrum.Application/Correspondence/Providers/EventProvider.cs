
namespace Spectrum.Application.Correspondence.Providers
{
    using Core.Services;
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
        protected readonly IEventRepository EventRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProvider" /> class.
        /// </summary>
        /// <param name="eventRepository">The event repository.</param>
        internal EventProvider(IEventRepository eventRepository)
        {
            this.EventRepository = eventRepository;
        }

        /// <summary>
        /// Record an event.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertEvent(EventModel model)
        {
            EventRepository.InsertEvent(model);
        }
    }
}
