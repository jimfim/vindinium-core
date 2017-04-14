using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Robot.Tactics
{
    public interface ITactic
    {
        IMapNode NextDestination();
    }
}