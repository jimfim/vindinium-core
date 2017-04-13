using System;
using System.Collections.Generic;
using System.Linq;
using vindiniumcore.Infrastructure.Behaviors.Map;
using vindiniumcore.Infrastructure.Behaviors.Movement;
using vindiniumcore.Infrastructure.Behaviors.Tactics;
using vindiniumcore.Infrastructure.DTOs;

namespace vindiniumcore.Infrastructure.Bots
{
    public class LoggingRobot : IBot
    {
        private readonly IPathFinding _pathFinding;
        private readonly ITactic _tactic;

        public LoggingRobot(ITactic tactic,IPathFinding pathFinding)
        {
            _tactic = tactic;
            _pathFinding = pathFinding;
        }

        public string BotName => "Robot";

        //starts everything
        public void Run(Server server)
        {
            while (server.Finished == false && server.Errored == false)
            {
                var destination = _tactic.NextDestination();
                var route = _pathFinding.GetShortestCompleteRouteToLocation(destination.Location);
                string direction = "Stay";
                if (route != null)
                {
                    
                    direction = server.GetDirection(server.MyHero.Location, route.Any() ? route.First().Location : null);
                }
                
                server.MoveHero(direction);
                Console.Clear();

                VisualizeMap(server, route);
                Console.Out.WriteLine("=========================================");
                Console.Out.WriteLine("Target Location : {0},{1}", destination.Location.X, destination.Location.Y);
                Console.Out.WriteLine("Target Cost \t: {0}", destination.MovementCost);
                Console.Out.WriteLine("Target Type \t: {0}", destination.Type);
                Console.Out.WriteLine("=========================================");
                Console.Out.WriteLine("Hero Location \t: {0},{1}", server.MyHero.Location.X, server.MyHero.Location.Y);
                Console.Out.WriteLine("Hero Life \t: {0}", (server.MyHero as HeroNode).Life);
                Console.Out.WriteLine("Hero Gold \t: {0}", (server.MyHero as HeroNode).Gold);
                Console.Out.WriteLine("Hero Mines \t: {0}", (server.MyHero as HeroNode).MineCount);
                Console.Out.WriteLine("Hero Moving \t: {0}", direction);
                Console.Out.WriteLine("=========================================");
                Console.Out.WriteLine("Completed Turn " + server.CurrentTurn);

            }

            if (server.Errored)
            {
                Console.Out.WriteLine("error: " + server.ErrorText);
            }
            Console.Out.WriteLine("{0} Finished", BotName);
        }


        private void VisualizeMap(Server server, List<IMapNode> route)
        {

            for (var x = 0; x < server.Board.Length; x++)
            {
                for (var y = 0; y < server.Board.Length; y++)
                {
                    Console.BackgroundColor = route.Any(i => i.Location.X == y && i.Location.Y == x) ? ConsoleColor.Red : ConsoleColor.Black;

                    switch (server.Board[y][x].Type)
                    {
                        case Tile.FREE:
                            Console.Write('_');
                            break;
                        case Tile.GOLD_MINE_1:
                        case Tile.GOLD_MINE_2:
                        case Tile.GOLD_MINE_3:
                        case Tile.GOLD_MINE_4:
                        case Tile.GOLD_MINE_NEUTRAL:
                            Console.Write('$');
                            break;
                        case Tile.HERO_1:
                        case Tile.HERO_2:
                        case Tile.HERO_3:
                        case Tile.HERO_4:
                            Console.Write('@');
                            break;
                        case Tile.TAVERN:
                            Console.Write('B');
                            break;
                        case Tile.IMPASSABLE_WOOD:
                            Console.Write('#');
                            break;
                        default:
                            Console.Write(' ');
                            break;
                    }
                }


                Console.WriteLine();
            }
        }

    }
}