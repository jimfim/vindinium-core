using Autofac;
using vindiniumcore.Infrastructure.Robot.Movement;

namespace vindiniumcore.Infrastructure.Modules
{
    public class PathFindingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShortestPath>().As<IPathFinding>();
            builder.RegisterType<ShortestPath>().Keyed<IPathFinding>(PathFindingStrategies.ShortestPath);
            
            base.Load(builder);
        }
    }
}
