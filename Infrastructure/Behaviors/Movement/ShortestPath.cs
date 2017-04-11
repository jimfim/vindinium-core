﻿using System.Collections.Generic;
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

        public ShortestPath(Server board)
        {
            _server = board;
            PopulateMovementCost();
        }


        public List<IMapNode> GetShortestCompleteRouteToLocation(CoOrdinates closestChest)
        {
            var result = new List<IMapNode>();
            var node = _server.Board[closestChest.X][closestChest.Y];
            int depth;
            IMapNode target = node;
            do
            {
                result.Add(target);
                depth = target.MovementCost;
                target =
                    target.Parents.Where(n => n.Passable && n.MovementCost > 0).OrderBy(n => n.MovementCost).First();
                // no route to anything protection.. just wait
                if (result.Count > 100)
                {
                    return null;
                }
            } while (depth > 1); // 0 is the Hero
            result = result.OrderBy(n => n.MovementCost).ToList();

            return result;
        }
        

        private void PopulateMovementCost()
	    {
	        int depth = 0;
	        Hero.MovementCost = depth;
	        depth++;

            foreach (var heroNode in Hero.Parents)
	        {
                AssignCost(depth, heroNode);
	            if (heroNode.Passable)
	            {
                    FindAllRoutes(depth, heroNode);
                }
	        }
	    }

	    private void FindAllRoutes(int depth, IMapNode parentMapNode)
	    {
            depth++;
            foreach (var node in parentMapNode.Parents.Where(n => n.MovementCost > depth))
            {
                AssignCost(depth, node);
                if (node.Passable)
                {
                    FindAllRoutes(depth, node);
                }
            }
        }

	    private void AssignCost(int cost, IMapNode mapNode)
	    {
            if (cost < mapNode.MovementCost)
            {
                if (mapNode.Type == Tile.IMPASSABLE_WOOD || mapNode.Type == _server.MyTreasure())
                {
                    mapNode.Passable = false;
                    mapNode.MovementCost = -1;
                }
                else if (_server.NotMyTreasure().Contains(mapNode.Type) ||
                    mapNode.Type == Tile.TAVERN ||
                    mapNode.Type == Tile.HERO_1 ||
                    mapNode.Type == Tile.HERO_2 ||
                    mapNode.Type == Tile.HERO_3 ||
                    mapNode.Type == Tile.HERO_4)
                {
                    mapNode.MovementCost = cost;
                    mapNode.Passable = false;
                }
                else
                {
                    mapNode.MovementCost = cost;
                    mapNode.Passable = true;
                }
            }
	    }
	}
}
