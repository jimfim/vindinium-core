using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Mappings
{
    public class VillianNodeResolverResolver : IValueResolver<GameResponse, GameDetails, IEnumerable<HeroNode>>
    {
        private readonly IMapper _mapper;

        public VillianNodeResolverResolver(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IEnumerable<HeroNode> Resolve(GameResponse source, GameDetails destination, IEnumerable<HeroNode> destMember, ResolutionContext context)
        {
            IEnumerable<Hero> heroes = source.game.heroes.Where(h => h.id != source.hero.id);
            return _mapper.Map<IEnumerable<HeroNode>>(heroes);
        }
    }
}