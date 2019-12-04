namespace SoundTraining.Provider.Modules
{
    using Autofac;
    using Dao.Implementations;
    using Dao.Interfaces;

    public class DatabaseModule : Module
    {
        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DbSessionFactory>().As<ISessionFactory>()
               .WithProperty("ConnectionString", ConnectionString)
               .SingleInstance();

            builder.RegisterType<HealthDao>().As<IHealthDao>().InstancePerDependency();
            builder.RegisterType<UserDao>().As<IUserDao>().InstancePerDependency();
            builder.RegisterType<HistoryDao>().As<IHistoryDao>().InstancePerDependency();
        }
    }
}
