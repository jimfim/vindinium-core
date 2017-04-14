using System;
using System.Collections.Generic;
using vindiniumcore.Infrastructure.DTOs;

namespace vindiniumcore.Infrastructure.Map
{
    public class VillianNode : IMapNode
    {
        public string Name;
        //Score
        public int Elo;

        public int Life;

        public int Gold;

        public int MineCount;

        public bool Crashed;

        public List<IMapNode> Parents { get; set; }

        public int Id { get; set; }

        public int MovementCost { get; set; }

        public CoOrdinates Location { get; set; }

        public Tile Type { get; set; }

        public bool Passable { get; set; }

        public VillianNode(Tile type, int x, int y)
        {
            Type = type;
            Passable = false;
            Location = new CoOrdinates(x, y);
            MovementCost = Int32.MaxValue;
        }
    }
}
