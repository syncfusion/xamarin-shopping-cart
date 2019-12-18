using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ShoppingAPI.Database;

namespace ShoppingAPI.App_Start
{
    /// <summary>
    /// Class for configure the Autofac in web API.
    /// </summary>
    public class AutofacWebapiConfig
    {
        /// <summary>
        /// This class is responsible for managing dependencies.
        /// </summary>
        public static IContainer Container;

        /// <summary>
        /// Initializes the configuration.
        /// </summary>
        /// <param name="config"></param>
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        /// <summary>
        /// Initializes the configuration and container.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="container"></param>
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        /// <summary>
        /// Register the services.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ShoppingContext>()
                .As<DbContext>()
                .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}