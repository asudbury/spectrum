namespace Spectrum.TestHarness
{
    using Content.Configuration;
    using Content.Registration.Providers;
    using Content.Registration.ViewModels;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IocConfiguration configuration = new IocConfiguration();
            configuration.RegisterSubscribers();

            ////TestRegistration();
        }

        /// <summary>
        /// Tests the registration.
        /// </summary>
        private void TestRegistration()
        {
            RegistrationProvider provider = new RegistrationProvider();

            RegisterViewModel viewModel = new RegisterViewModel
            {
                Name = "Adrian",
                Password = "password",
                EmailAddress = "a@a.com"
            };

            provider.RegisterUser(viewModel);
        }
    }
}
