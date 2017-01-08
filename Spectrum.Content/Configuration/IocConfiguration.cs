namespace Spectrum.Content.Configuration
{
    using Registration.Models;
    using Registration.Handlers;
    using System.Web.Http;
    using TinyIoC;
    using TinyMessenger;
    
    /// <summary>
    /// Defines the IocConfiguration.
    /// </summary>
    public class IocConfiguration
    {
        /// <summary>
        /// Setups this instance.
        /// </summary>
        public void Setup(HttpConfiguration config)
        {
            ////config.DependencyResolver = new TinyIocMvcDependencyResolver(GetContainer());
             
            RegisterServices();
            RegisterSubscribers();
        }

        /// <summary>
        /// Register the services.
        /// </summary>
        public void RegisterServices()
        {
        }

        /// <summary>
        /// Register the subscribers.
        /// </summary>
        public void RegisterSubscribers()
        {
            ITinyMessengerHub messengerHub = GetContainer().Resolve<ITinyMessengerHub>();

            messengerHub.Subscribe<RegistrationCompleteMessage>(m => { new RegistrationHandler().Handle(m); });
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <returns>The Container.</returns>
        private TinyIoCContainer GetContainer()
        {
            return TinyIoCContainer.Current;
        }
    }
}
