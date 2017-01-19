namespace Spectrum.Application.Correspondence.Providers
{
    using Model.Correspondence;

    /// <summary>
    /// The IEventProvider interface.
    /// </summary>
    public interface IEventProvider
    {
        /// <summary>
        /// Email has been sent.
        /// </summary>
        /// <param name="model">The model.</param>
        void InsertEvent(EventModel model);
    }
}
