using Autofac;
using vindiniumcore.Infrastructure.Robot.Tactics;

namespace vindiniumcore.Infrastructure.Modules
{
    public class TacticsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GoldRush>().As<ITactic>();
            builder.RegisterType<DumbGoldRush>().Keyed<ITactic>(Tactics.DumbGoldRush);
            builder.RegisterType<GoldRush>().Keyed<ITactic>(Tactics.SurvivalGoldRush);

            base.Load(builder);
        }
    }
}
