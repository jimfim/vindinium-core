using Autofac;
using vindiniumcore.Infrastructure.Behaviors.Movement;

namespace vindiniumcore.Infrastructure.Modules
{
    public class PathFindingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShortestPath>().Keyed<IPathFinding>(PathFindingStrategies.AStar);
            
            base.Load(builder);
        }
    }
}
