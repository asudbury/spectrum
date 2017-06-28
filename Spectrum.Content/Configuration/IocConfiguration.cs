namespace Spectrum.Content.Configuration
{
    using Appointments.Controllers;
    using Autofac;
    using Autofac.Integration.Mvc;
    using System.Reflection;
    using System.Web.Mvc;

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

            //// As long as all the controllers are in the same assembly this will work.

            Assembly assembly = typeof(AppointmentsController).Assembly;

            builder.RegisterControllers(assembly);

            
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            IContainer container = builder.Build();

            //// Set the dependency resolver to be Autofac.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
