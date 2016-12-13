namespace Spectrum.Content.Configuration
{
    using Registration.Handlers;
    using Registration.Messages;
    using System.Web.Http;
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
            ////config.DependencyResolver = new TinyIoCDependencyResolver(container);
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
            ITinyMessengerHub messengerHub = TinyIoC.TinyIoCContainer.Current.Resolve<ITinyMessengerHub>();

            messengerHub.Subscribe<RegistrationMessage>(m => { new RegistrationHandler().Handle(m); });
        }
    }
}
