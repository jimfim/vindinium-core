using System.Collections.Generic;
using System.Linq;
using vindiniumcore.Infrastructure.Map;
using vindiniumcore.Infrastructure.Services;

namespace vindiniumcore.Infrastructure.Robot.Movement
{
    /// <summary>
    /// A* search
    /// </summary>
	public class ShortestPath : IPathFinding
    {
        private readonly IGameService _gameService;

        public ShortestPath(IGameService gameService)
        {
            _gameService = gameService;
        }


        public List<IMapNode> GetShortestCompleteRouteToLocation(CoOrdinates closestChest)
        {
            var game = _gameService.GetGame();
            var result = new List<IMapNode>();
            var node = game.Board[closestChest.X][closestChest.Y];
            IMapNode target = node;
            try
            {
                int depth;
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
