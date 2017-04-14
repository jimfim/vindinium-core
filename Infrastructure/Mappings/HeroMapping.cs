using AutoMapper;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Mappings
{
    public class HeroMapping : Profile
    {
        public HeroMapping()
        {
            CreateMap<Hero, HeroNode>()
                .ConstructUsing(o => new HeroNode(GetTileType(o), o.pos.y, o.pos.x)
                {
                    Id = o.id,
                    Elo = o.elo,
                    Gold = o.gold,
                    Life = o.life,
                    Passable = false,
                    Crashed = o.crashed,
                    MineCount = o.mineCount
                })
                .ForAllMembers(o => o.Ignore());


            CreateMap<Hero, VillianNode>()
                .ConstructUsing(o => new VillianNode(GetTileType(o), o.pos.y, o.pos.x)
                {
                    Id = o.id,
                    Elo = o.elo,
                    Gold = o.gold,
                    Life = o.life,
                    Passable = false,
                    Crashed = o.crashed,
                    MineCount = o.mineCount
                })
                .ForAllMembers(o => o.Ignore());
        }

        private Tile GetTileType(Hero hero)
        {
            switch (hero.id)
            {
                case 1:
                    return Tile.HERO_1;
                case 2:
                    return Tile.HERO_2;
                case 3:
                    return Tile.HERO_3;
                case 4:
                    return Tile.HERO_4;
                default:
                    return Tile.FREE;
            }
        }
    }
}