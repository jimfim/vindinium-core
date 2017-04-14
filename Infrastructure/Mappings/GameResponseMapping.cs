using AutoMapper;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Mappings
{
    class GameResponseMapping : Profile
    {
        public GameResponseMapping()
        {
            CreateMap<GameResponse, GameDetails>()
                .ForMember(dest => dest.MaxTurns, opt => opt.MapFrom(src => src.game.maxTurns))
                .ForMember(dest => dest.CurrentTurn, opt => opt.MapFrom(src => src.game.turn))
                .ForMember(dest => dest.Finished, opt => opt.MapFrom(src => src.game.finished))
                .ForMember(dest => dest.AllCharacters, opt => opt.MapFrom(src => src.game.heroes))
                .ForMember(dest => dest.MyHero, opt => opt.MapFrom(src => src.hero))
                .ForMember(dest => dest.Board, opt => opt.MapFrom(src => src.game.board))
                .ForMember(dest => dest.Villians, , opt => opt.ResolveUsing<VillianNodeResolverResolver>())
                .ForMember(dest => dest.Taverns, opt => opt.MapFrom(src => src.game.heroes))
                .ForMember(dest => dest.Chests, opt => opt.MapFrom(src => src.game.heroes));

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

    internal class VillianNodeResolverResolver
    {
    }

    internal class BoardBuilderConvert : ITypeConverter<Board, IMapNode[][]>
    {
        public IMapNode[][] Convert(Board source, IMapNode[][] destination, ResolutionContext context)
        {
            return CreateBoard(source.size, source.tiles);
        }


        private IMapNode[][] CreateBoard(int size, string data)
        {
            IMapNode[][] board = { };
            //check to see if the Board list is already created, if it is, we just overwrite its values
            if (board.Length != size)
            {
                board = new IMapNode[size][];
                for (var index = 0; index < board.Length; index++)
                {
                    board[index] = new IMapNode[size];
                }
            }

            var x = 0;
            var y = 0;
            var charData = data.ToCharArray();

            for (var i = 0; i < charData.Length; i += 2)
            {
                switch (charData[i])
                {
                    case '#':
                        board[x][y] = new MapNode(Tile.IMPASSABLE_WOOD, x, y);
                        break;
                    case ' ':
                        board[x][y] = new MapNode(Tile.FREE, x, y);
                        break;
                    case '@':
                        switch (charData[i + 1])
                        {
                            case '1':
                                board[x][y] = new MapNode(Tile.HERO_1, x, y);
                                break;
                            case '2':
                                board[x][y] = new MapNode(Tile.HERO_2, x, y);
                                break;
                            case '3':
                                board[x][y] = new MapNode(Tile.HERO_3, x, y);
                                break;
                            case '4':
                                board[x][y] = new MapNode(Tile.HERO_4, x, y);
                                break;
                        }
                        break;
                    case '[':
                        board[x][y] = new MapNode(Tile.TAVERN, x, y);

                        break;
                    case '$':
                        switch (charData[i + 1])
                        {
                            case '-':
                                board[x][y] = new MapNode(Tile.GOLD_MINE_NEUTRAL, x, y);
                                break;
                            case '1':
                                board[x][y] = new MapNode(Tile.GOLD_MINE_1, x, y);
                                break;
                            case '2':
                                board[x][y] = new MapNode(Tile.GOLD_MINE_2, x, y);
                                break;
                            case '3':
                                board[x][y] = new MapNode(Tile.GOLD_MINE_3, x, y);
                                break;
                            case '4':
                                board[x][y] = new MapNode(Tile.GOLD_MINE_4, x, y);
                                break;
                        }
                        break;
                }

                //time to increment X and Y
                x++;
                if (x == size)
                {
                    x = 0;
                    y++;
                }
            }
            return board;
        }
    }
}
