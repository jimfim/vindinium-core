using System.Linq;
using vindiniumcore.Infrastructure.Robot.Movement;
using vindiniumcore.Infrastructure.Robot.Tactics;
using vindiniumcore.Infrastructure.Services;
using vindiniumcore.Infrastructure.Services.ApiClient;

namespace vindiniumcore.Infrastructure.Robot.Bots
{
    public class FighterBot : IBot
    {
        private readonly ITactic _tactic;
        private readonly IPathFinding _pathFinding;
        private readonly Server _server;
        private readonly IVindiniumClient _vindiniumClient;
        private readonly IGameService _gameService;
        public string BotName => "Thunder Budddy";

        public FighterBot(ITactic tactic, IPathFinding pathFinding, Server server, IVindiniumClient vindiniumClient, IGameService gameService)
        {
            _tactic = tactic;
            _pathFinding = pathFinding;
            _server = server;
            _vindiniumClient = vindiniumClient;
            _gameService = gameService;
        }

        public void Run()
        {
            _vindiniumClient.CreateGame();
            while (_server.Finished == false && _server.Errored == false)
            {
                var game = _gameService.GetGame();
                var route = _pathFinding.GetShortestCompleteRouteToLocation(_tactic.NextDestination().Location);
                string direction = "Stay";
                if (route != null)
                {
                    direction = _server.GetDirection(game.MyHero.Location, route.Any() ? route.First().Location : null);
                }
                _server.MoveHero(direction);            
            }
        }
    }
}
