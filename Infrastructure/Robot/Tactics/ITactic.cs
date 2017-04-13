using vindiniumcore.Infrastructure.Behaviors.Map;

namespace vindiniumcore.Infrastructure.Behaviors.Tactics
{
    public interface ITactic
    {
        IMapNode NextDestination();
    }
}