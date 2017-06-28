using Spectrum.Content.Configuration;
using Umbraco.Core;
using Umbraco.Web.Routing;

namespace Spectrum.Umbraco
{
    using Content.Handlers;

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
            ContentFinderResolver.Current.InsertTypeBefore<ContentFinderByNotFoundHandlers, Error404Handler>();

            //// do autofac wiring up

            IocConfiguration.Setup();

            base.ApplicationStarting(umbracoApplication, applicationContext);
        }
    }
}