using System.Collections.Generic;
using System.Linq;
using vindiniumcore.Infrastructure.Behaviors.Map;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Extensions;

namespace vindiniumcore.Infrastructure.Behaviors.Movement
{
    /// <summary>
    /// A* search
    /// </summary>
	public class ShortestPath : IPathFinding
    {
		private readonly Server _server;

	    private IMapNode Hero => _server.MyHero;       

        public ShortestPath(Server server)
        {
            _server = server;
        }


        public List<IMapNode> GetShortestCompleteRouteToLocation(CoOrdinates closestChest)
        {
            var result = new List<IMapNode>();
            var node = _server.Board[closestChest.X][closestChest.Y];
            int depth;
            IMapNode target = node;
            try
            {
                do
                {
                    result.Add(target);
                    depth = target.MovementCost;
                    target =
                        target.Parents.Where(n => n.Passable && n.MovementCost > 0)
                            .OrderBy(n => n.MovementCost)
                            .First();
                    // no route to anything protection.. just wait
                    if (result.Count > 100)
                    {
                        return null;
                    }
                } while (depth > 1); // 0 is the Hero.

                result = result.OrderBy(n => n.MovementCost).ToList();
            }
            catch
            {
            }


            return result;
        }
        


	    
	}
}
