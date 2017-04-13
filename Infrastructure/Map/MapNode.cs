using System;
using System.Collections.Generic;
using vindiniumcore.Infrastructure.DTOs;

namespace vindiniumcore.Infrastructure.Behaviors.Map
{
	public class MapNode : IMapNode
	{
		public int Id { get; set; }
		
		public int MovementCost { get; set; }

		public Tile Type { get; set; }

		public bool Passable { get; set; }

	    public List<IMapNode> Parents { get; set; }

		public CoOrdinates Location { get; }

		public MapNode(Tile type, int x, int y)
		{
			Type = type;
			Passable = false;
			Location = new CoOrdinates(x, y);
            MovementCost = Int32.MaxValue;
        }
	}
}
