using Autofac;
using vindiniumcore.Infrastructure.Behaviors.Tactics;

namespace vindiniumcore.Infrastructure.Modules
{
    public class TacticsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DumbGoldRush>().Keyed<ITactic>(Tactics.DumbGoldRush);
            builder.RegisterType<SurvivalGoldRush>().Keyed<ITactic>(Tactics.SurvivalGoldRush);

            base.Load(builder);
        }
    }
}
