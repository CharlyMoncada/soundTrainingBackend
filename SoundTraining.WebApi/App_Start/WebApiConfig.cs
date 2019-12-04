namespace SoundTraining.WebApi
{
    using Autofac;
    using Autofac.Configuration;
    using Autofac.Integration.WebApi;
    using Microsoft.Extensions.Configuration;
    using Provider.Modules;
    using Provider.Services.Implementations;
    using Provider.Services.Interfaces;
    using System.Reflection;
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = DeclareContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer DeclareContainer()
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("autofac.json");
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new ConfigurationModule(builder.Build()));
            containerBuilder.RegisterModule(new LoggingModule());
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            containerBuilder.RegisterType<DaoService>()
                .As<IDaoService>()
                .InstancePerDependency();

            return containerBuilder.Build();

        }
    }
}
