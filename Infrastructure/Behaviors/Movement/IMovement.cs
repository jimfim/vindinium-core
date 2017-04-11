using System.Collections.Generic;
using vindiniumcore.Infrastructure.Behaviors.Map;

namespace vindiniumcore.Infrastructure.Behaviors.Movement
{
	public interface IPathFinding
	{
        List<IMapNode> GetShortestCompleteRouteToLocation(CoOrdinates closestChest);
    }
}