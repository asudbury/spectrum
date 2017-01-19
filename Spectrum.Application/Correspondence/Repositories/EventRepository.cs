namespace Spectrum.Application.Correspondence.Repositories
{
    using Model.Correspondence;
    using NPoco;

    internal class EventRepository : IEventRepository
    {
        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="model">The model.</param>
        void IEventRepository.InsertEvent(EventModel model)
        {
            //// TODO : we want to read the connection string from web/app.config at some point
            IDatabase db = new Database("connStringName");
            db.Insert(model);
        }
    }
}
