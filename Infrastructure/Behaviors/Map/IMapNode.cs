using System.Collections.Generic;
using vindiniumcore.Infrastructure.DTOs;

namespace vindiniumcore.Infrastructure.Behaviors.Map
{
    public interface IMapNode
    {
        List<IMapNode> Parents { get; set; }

        int Id { get; set; }

        int MovementCost { get; set; }

        CoOrdinates Location { get; }

        Tile Type { get; }

        bool Passable { get; set; }
    }
}