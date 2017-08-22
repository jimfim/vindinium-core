using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Mappings
{
    public class VillianNodeResolverResolver : IValueResolver<GameResponse, GameDetails, IEnumerable<IMapNode>>
    {
        private readonly IMapper _mapper;

        public VillianNodeResolverResolver(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IEnumerable<IMapNode> Resolve(GameResponse source, GameDetails destination, IEnumerable<IMapNode> destMember, ResolutionContext context)
        {
            IEnumerable<Hero> test = source.game.heroes.Where(h => h.id != source.hero.id);
            var narf = _mapper.Map<IEnumerable<VillianNode>>(test);
            return narf;
        }
    }
}