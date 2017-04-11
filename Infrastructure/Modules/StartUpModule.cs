using Autofac;

namespace vindiniumcore.Infrastructure.Modules
{
    class StartUpModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Startup>().As<IStartable>().SingleInstance();
            base.Load(builder);
        }
    }
}
