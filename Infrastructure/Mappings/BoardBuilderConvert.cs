using AutoMapper;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Mappings
{
    public class BoardBuilderConvert : ITypeConverter<Board, IMapNode[][]>
    {
        private IMapNode[][] _gameBoard = { };

        public IMapNode[][] Convert(Board source, IMapNode[][] destination, ResolutionContext context)
        {
            CreateBoard(source.size, source.tiles);
            return _gameBoard;
        }

        private void CreateBoard(int size, string data)
        {
            //check to see if the Board list is already created, if it is, we just overwrite its values
            if (_gameBoard.Length != size)
            {
                _gameBoard = new IMapNode[size][];
                for (var index = 0; index < _gameBoard.Length; index++)
                {
                    _gameBoard[index] = new IMapNode[size];
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
                        _gameBoard[x][y] = new MapNode(Tile.IMPASSABLE_WOOD, x, y);
                        break;
                    case ' ':
                        _gameBoard[x][y] = new MapNode(Tile.FREE, x, y);
                        break;
                    case '@':
                        switch (charData[i + 1])
                        {
                            case '1':
                                _gameBoard[x][y] = new MapNode(Tile.HERO_1, x, y);
                                break;
                            case '2':
                                _gameBoard[x][y] = new MapNode(Tile.HERO_2, x, y);
                                break;
                            case '3':
                                _gameBoard[x][y] = new MapNode(Tile.HERO_3, x, y);
                                break;
                            case '4':
                                _gameBoard[x][y] = new MapNode(Tile.HERO_4, x, y);
                                break;
                        }
                        break;
                    case '[':
                        _gameBoard[x][y] = new MapNode(Tile.TAVERN, x, y);

                        break;
                    case '$':
                        switch (charData[i + 1])
                        {
                            case '-':
                                _gameBoard[x][y] = new MapNode(Tile.GOLD_MINE_NEUTRAL, x, y);
                                break;
                            case '1':
                                _gameBoard[x][y] = new MapNode(Tile.GOLD_MINE_1, x, y);
                                break;
                            case '2':
                                _gameBoard[x][y] = new MapNode(Tile.GOLD_MINE_2, x, y);
                                break;
                            case '3':
                                _gameBoard[x][y] = new MapNode(Tile.GOLD_MINE_3, x, y);
                                break;
                            case '4':
                                _gameBoard[x][y] = new MapNode(Tile.GOLD_MINE_4, x, y);
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
        }

        //private void PopulateMovementCost()
        //{
        //    int depth = 0;
        //    //gameService.GetGame().MyHero.MovementCost = depth;
        //    depth++;

        //    foreach (var heroNode in gameMyHero.Parents)
        //    {
        //        AssignCost(depth, heroNode);
        //        if (heroNode.Passable)
        //        {
        //            FindAllRoutes(depth, heroNode);
        //        }
        //    }
        //}

        //private void FindAllRoutes(int depth, IMapNode parentMapNode)
        //{
        //    depth++;
        //    foreach (var node in parentMapNode.Parents.Where(n => n.MovementCost > depth))
        //    {
        //        AssignCost(depth, node);
        //        if (node.Passable)
        //        {
        //            FindAllRoutes(depth, node);
        //        }
        //    }
        //}

        //private void AssignCost(int cost, IMapNode mapNode)
        //{
        //    if (cost < mapNode.MovementCost)
        //    {
        //        if (mapNode.Type == Tile.IMPASSABLE_WOOD || mapNode.Type == MyTreasure())
        //        {
        //            mapNode.Passable = false;
        //            mapNode.MovementCost = -1;
        //        }
        //        else if (NotMyTreasure().Contains(mapNode.Type) ||
        //                 mapNode.Type == Tile.HERO_1 ||
        //                 mapNode.Type == Tile.HERO_2 ||
        //                 mapNode.Type == Tile.HERO_3 ||
        //                 mapNode.Type == Tile.HERO_4)
        //        {
        //            mapNode.MovementCost = cost;
        //            mapNode.Passable = false;
        //        }
        //        else
        //        {
        //            mapNode.MovementCost = cost;
        //            mapNode.Passable = true;
        //        }
        //    }
        //}

        //private void PopulateNodeParents()
        //{
        //    foreach (IMapNode[] t in _gameBoard)
        //    {
        //        foreach (IMapNode t1 in t)
        //        {
        //            var node = t1;
        //            var parents = GetParents(t1);
        //            node.Parents = parents;
        //        }
        //    }
        //}

        //private List<IMapNode> GetParents(IMapNode sourceMapNode)
        //{
        //    var results = new List<IMapNode>();
        //    if (sourceMapNode.Location.Y - 1 >= 0)
        //    {
        //        var north = _gameBoard[sourceMapNode.Location.X][sourceMapNode.Location.Y - 1];
        //        results.Add(north);
        //    }

        //    if (sourceMapNode.Location.Y + 1 <= _gameBoard.Length - 1)
        //    {
        //        var south = _gameBoard[sourceMapNode.Location.X][sourceMapNode.Location.Y + 1];
        //        results.Add(south);
        //    }

        //    if (sourceMapNode.Location.X + 1 <= _gameBoard.Length - 1)
        //    {
        //        var east = _gameBoard[sourceMapNode.Location.X + 1][sourceMapNode.Location.Y];
        //        results.Add(east);
        //    }

        //    if (sourceMapNode.Location.X - 1 >= 0)
        //    {
        //        var west = _gameBoard[sourceMapNode.Location.X - 1][sourceMapNode.Location.Y];
        //        results.Add(west);
        //    }
        //    return results;
        //}

    }
}