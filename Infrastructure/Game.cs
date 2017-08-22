using System.Collections.Generic;
using System.Linq;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Map;
using vindiniumcore.Infrastructure.Mappings;

namespace vindiniumcore.Infrastructure
{
    /// <summary>
    /// <see cref="GameResponseMapping"/>
    /// </summary>
    public class GameDetails
    {
        public HeroNode MyHero { get; set; }

        public List<HeroNode> AllCharacters { get; set; }

        public IEnumerable<HeroNode> Villians { get; set; }

        public int CurrentTurn { get; set; }

        public int MaxTurns { get; set; }

        public bool Finished { get; set; }

        public bool Errored { get; set; }

        public IMapNode[][] Board { get; set; }

        public object Taverns { get; set; }

        public object Chests { get; set; }




        public List<Tile> NotMyTreasure()
        {
            var tileset = new List<Tile> { Tile.GOLD_MINE_1, Tile.GOLD_MINE_2, Tile.GOLD_MINE_3, Tile.GOLD_MINE_4, Tile.GOLD_MINE_NEUTRAL };
            var mytreasure = MyTreasure();
            tileset.Remove(mytreasure);
            return tileset;
        }

        public List<Tile> NotMe()
        {
            var tileset = new List<Tile> { Tile.HERO_1, Tile.HERO_2, Tile.HERO_3, Tile.HERO_4 };
            tileset.Remove(MyHero.Type);
            return tileset;
        }

        public Tile MyTreasure()
        {
            switch (MyHero.Type)
            {
                case Tile.HERO_1:
                    return Tile.GOLD_MINE_1;
                case Tile.HERO_2:
                    return Tile.GOLD_MINE_2;
                case Tile.HERO_3:
                    return Tile.GOLD_MINE_3;
                case Tile.HERO_4:
                    return Tile.GOLD_MINE_4;
                default:
                    return Tile.GOLD_MINE_1;
            }
        }

        /// <summary>
        /// returns closest *accessible* gold mine,and null if none available
        /// </summary>
        /// <param name="gameDetails"></param>
        /// <returns></returns>
        public IMapNode GetClosestChest()
        {
            var viableChests = Find(NotMyTreasure());
            IMapNode closest = null;
            if (viableChests.Any())
            {
                closest = viableChests.OrderBy(c => c.MovementCost).First();
            }
            return closest;
        }

        /// <summary>
        /// returns closest *accessible* gold mine,and null if none available
        /// </summary>
        /// <param name="gameDetails"></param>
        /// <returns></returns>
        public IMapNode GetClosestTavern()
        {
            IMapNode closest = null;
            var viableTaverns = Find(Tile.TAVERN);
            if (viableTaverns.Any())
            {
                closest = viableTaverns.OrderBy(c => c.MovementCost).First();
            }
            return closest;
        }

        public IMapNode GetClosestEnemy()
        {
            IMapNode closest = null;
            var closestEnemy = Find(NotMe());
            if (closestEnemy.Any())
            {
                closest = closestEnemy.OrderBy(c => c.MovementCost).First();
            }
            return closest;
        }

        private List<IMapNode> Find(Tile tile)
        {
            var tileset = new List<Tile> { tile };
            return Find(tileset);
        }

        private List<IMapNode> Find(List<Tile> tileset)
        {
            var viableTargets = new List<IMapNode>();
            for (int y = 0; y < Board.Length; y++)
            {
                foreach (IMapNode[] t in Board)
                {
                    viableTargets.AddRange(tileset.Where(tile => t[y].Type == tile).Select(tile => t[y]));
                }
            }

            return viableTargets;
        }
    }
}