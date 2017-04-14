using System.Collections.Generic;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure
{
    public class GameDetails
    {
        public IMapNode MyHero { get; set; }

        public List<IMapNode> AllCharacters { get; set; }

        public IEnumerable<IMapNode> Villians { get; set; }

        public int CurrentTurn { get; set; }

        public int MaxTurns { get; set; }

        public bool Finished { get; set; }

        public bool Errored { get; set; }

        public IMapNode[][] Board { get; set; }
        public object Taverns { get; set; }
        public object Chests { get; set; }
    }
}