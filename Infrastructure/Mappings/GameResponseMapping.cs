using AutoMapper;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Mappings
{
    public class GameResponseMapping : Profile
    {
        public GameResponseMapping()
        {
            CreateMap<GameResponse, GameDetails>()
                .ForMember(dest => dest.MaxTurns, opt => opt.MapFrom(src => src.game.maxTurns))
                .ForMember(dest => dest.CurrentTurn, opt => opt.MapFrom(src => src.game.turn))
                .ForMember(dest => dest.Finished, opt => opt.MapFrom(src => src.game.finished))
                .ForMember(dest => dest.AllCharacters, opt => opt.MapFrom(src => src.game.heroes))
                .ForMember(dest => dest.AllCharacters, opt => opt.Ignore())
                .ForMember(dest => dest.MyHero, opt => opt.Ignore())
                .ForMember(dest => dest.Board, opt => opt.MapFrom(src => src.game.board))
                .ForMember(dest => dest.Villians, opt => opt.ResolveUsing<VillianNodeResolverResolver>())
                .ForMember(dest => dest.Taverns, opt => opt.Ignore())
                .ForMember(dest => dest.Chests, opt => opt.Ignore());

            CreateMap<Hero, IMapNode>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.MovementCost, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Parents, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Passable, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

            CreateMap<Board, IMapNode[][]>()
                .ConvertUsing<BoardBuilderConvert>();
                
        }
    }
}
