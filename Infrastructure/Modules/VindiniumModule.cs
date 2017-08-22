using Autofac;
using vindiniumcore.Infrastructure.Robot.Bots;
using vindiniumcore.Infrastructure.Services;
using vindiniumcore.Infrastructure.Services.ApiClient;

namespace vindiniumcore.Infrastructure.Modules
{
    public class VindiniumModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VindiniumClient>().As<IVindiniumClient>();
            builder.RegisterType<Server>().SingleInstance();
            builder.RegisterType<GameService>().As<IGameService>().SingleInstance();
            builder.RegisterType<FighterBot>().As<IBot>();
            builder.RegisterType<VindiniumSettings>().SingleInstance();
            base.Load(builder);
        }
    }
}
