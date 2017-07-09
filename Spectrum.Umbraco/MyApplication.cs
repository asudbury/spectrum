using Umbraco.Core;

namespace Spectrum.Umbraco
{
    using Content.Configuration;

    public class MyApplication : ApplicationEventHandler
    {
        /// <summary>
        /// Overridable method to execute when All resolvers have been initialized but resolution is not frozen so they can be modified in this method
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        protected override void ApplicationStarting(
            UmbracoApplicationBase umbracoApplication, 
            ApplicationContext applicationContext)
        {
            IocConfiguration.Setup();

            base.ApplicationStarting(umbracoApplication, applicationContext);
        }

        /// <summary>
        /// Overridable method to execute when Bootup is completed, this allows you to perform any other bootup logic required for the application.
        /// Resolution is frozen so now they can be used to resolve instances.
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        protected override void ApplicationStarted(
            UmbracoApplicationBase umbracoApplication, 
            ApplicationContext applicationContext)
        {
            Application.Started(umbracoApplication, applicationContext);

            base.ApplicationStarted(umbracoApplication, applicationContext);
        }
    }
}