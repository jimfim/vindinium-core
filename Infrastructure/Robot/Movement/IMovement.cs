using System.Collections.Generic;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Robot.Movement
{
	public interface IPathFinding
	{
        List<IMapNode> GetShortestCompleteRouteToLocation(CoOrdinates closestChest);
    }
}