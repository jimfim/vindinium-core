using Autofac;
using vindiniumcore.Infrastructure.Services.ApiClient;

namespace vindiniumcore.Infrastructure.Modules
{
    public class VindiniumModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VindiniumClient>().As<IVindiniumClient>();
            builder.RegisterType<Server>();
            builder.RegisterType<VindiniumSettings>();
            base.Load(builder);
        }
    }
}
