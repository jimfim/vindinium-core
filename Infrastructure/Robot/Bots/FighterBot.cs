using System.Linq;
using vindiniumcore.Infrastructure.Robot.Movement;
using vindiniumcore.Infrastructure.Robot.Tactics;

namespace vindiniumcore.Infrastructure.Robot.Bots
{
    class FighterBot : IBot
    {
        private readonly ITactic _tactic;
        private readonly IPathFinding _pathFinding;
        private readonly Server _server;
        public string BotName => "Thunder Budddy";

        public FighterBot(ITactic tactic, IPathFinding pathFinding, Server server)
        {
            _tactic = tactic;
            _pathFinding = pathFinding;
            _server = server;
        }

        public void Run(GameDetails gameDetails)
        {
            while (_server.Finished == false && _server.Errored == false)
            {
                var destination = _tactic.NextDestination();
                var route = _pathFinding.GetShortestCompleteRouteToLocation(destination.Location);

                string direction = "Stay";
                if (route != null)
                {

                    direction = _server.GetDirection(_server.MyHero.Location,
                        route.Any() ? route.First().Location : null);
                }

                _server.MoveHero(direction);
            }
        }
    }
}
