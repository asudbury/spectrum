namespace Spectrum.Application.Correspondence.Repositories
{
    using Model.Correspondence;

    internal interface IEventRepository
    {
        /// <summary>
        /// Inserts the event.
        /// </summary>
        /// <param name="model">The model.</param>
        void InsertEvent(EventModel model);
    }
}
