using Spectrum.Application.Services;

namespace Spectrum.Content.Configuration
{
    using Appointments.Controllers;
    using Autofac;
    using Autofac.Features.Variance;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using System.Reflection;
    using System.Web.Mvc;
    using Umbraco.Web;

    /// <summary>
    /// Defines the IocConfiguration.
    /// </summary>
    public static class IocConfiguration
    {
        /// <summary>
        /// Setups this instance.
        /// </summary>
        public static void Setup()
        {
            ContainerBuilder builder = new ContainerBuilder();

            //// Set up publish and subscribe.
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterEventing();

            Assembly assembly = typeof(AppointmentsController).Assembly;

            builder.RegisterControllers(assembly);

            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            //// this will register all the interfaces in the application assembly.
            builder.RegisterAssemblyTypes(typeof(CacheService).Assembly).AsImplementedInterfaces();

            IContainer container = builder.Build();

            //// Set the dependency resolver to be Autofac.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
