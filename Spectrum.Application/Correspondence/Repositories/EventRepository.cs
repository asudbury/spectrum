namespace Spectrum.Application.Correspondence.Repositories
{
    using Core.Services;
    using Model.Correspondence;
    using NPoco;

    internal class EventRepository : IEventRepository
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventRepository"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        public EventRepository(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="model">The model.</param>
        void IEventRepository.InsertEvent(EventModel model)
        {
            using (IDatabase db = new Database(databaseService.CorrespondenceConnectionString))
            {
                db.Insert(model);
            }
        }
    }
}
