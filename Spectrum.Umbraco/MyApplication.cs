using System.Linq;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Logging;

namespace Spectrum.Umbraco
{
    using Content.Configuration;

    public class MyApplication : ApplicationEventHandler
    {
        /// <inheritdoc />
        /// <summary>
        /// Overridable method to execute when All resolvers have been initialized but resolution is not frozen so they can be modified in this method
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        protected override void ApplicationStarting(
            UmbracoApplicationBase umbracoApplication, 
            ApplicationContext applicationContext)
        {
            LogHelper.Info(GetType(), "ApplicationStarting");

            IocConfiguration.Setup();

            base.ApplicationStarting(umbracoApplication, applicationContext);

            RazorViewEngine razorEngine = ViewEngines.Engines.OfType<RazorViewEngine>().FirstOrDefault();

            if (razorEngine != null)
            {
                razorEngine.PartialViewLocationFormats =
                    razorEngine.PartialViewLocationFormats.Concat(new[]
                    {
                        "~/Views/Partials/Spectrum/Payments/{0}.cshtml",
                        "~/Views/Partials/Spectrum/Appointments/{0}.cshtml"
                    }).ToArray();
            }
        }

        /// <inheritdoc />
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
            LogHelper.Info(GetType(), "ApplicationStarted");

            ApplicationConfiguration.Started(umbracoApplication, applicationContext);

            base.ApplicationStarted(umbracoApplication, applicationContext);
        }
    }
}