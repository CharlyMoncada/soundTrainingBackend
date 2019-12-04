namespace SoundTraining.Provider.Modules
{
    using Autofac;
    using Autofac.Core;
    using log4net;
    using log4net.Core;
    using log4net.Repository.Hierarchy;
    using System.Linq;
    using System.Reflection;

    public class LoggingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => LogManager.GetLogger(typeof(object))).As<ILog>();
            //builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
            log4net.Config.XmlConfigurator.Configure();
        }

        protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        {
            registration.Preparing +=
                (sender, args) =>
                {
                    args.Parameters = args.Parameters.Union(
                                      new[]
                                      {
                                            //new ResolvedParameter(
                                            //(p, i) => p.ParameterType == typeof(ILogger),
                                            //(p, i) => i.Resolve<ILoggerFactory>().GetLogger(p.Member.DeclaringType)),
                                            new ResolvedParameter(
                                            (p, i) => p.ParameterType == typeof(ILog),
                                            (p, i) => LogManager.GetLogger(p.Member.DeclaringType))
                                      });
                };
            registration.Activated += (sender, e) => InjectLoggerProperties(e);
        }

        private static void InjectLoggerProperties(ActivatedEventArgs<object> e)
        {
            var instance = e.Instance;
            var instanceType = instance.GetType();

            //var properties = instanceType
            //  .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            //  .Where(p => p.PropertyType == typeof(ILogger) && p.CanWrite && p.GetIndexParameters().Length == 0);

            //foreach (var propToSet in properties)
            //{
            //    propToSet.SetValue(instance, e.Context.Resolve<ILoggerFactory>().GetLogger(instanceType), null);
            //}

            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(ILog) && p.CanWrite && p.GetIndexParameters().Length == 0);

            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, LogManager.GetLogger(instanceType), null);
            }
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(
              new[]
              {
                //new ResolvedParameter(
                //(p, i) => p.ParameterType == typeof(ILogger),
                //(p, i) => i.Resolve<ILoggerFactory>().GetLogger(p.Member.DeclaringType)),
                new ResolvedParameter(
                (p, i) => p.ParameterType == typeof(ILog),
                (p, i) => LogManager.GetLogger(p.Member.DeclaringType))
              });
        }
    }
}
