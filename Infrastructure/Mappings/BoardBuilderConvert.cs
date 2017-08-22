using AutoMapper;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Mappings
{
    public class BoardBuilderConvert : ITypeConverter<Board, IMapNode[][]>
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