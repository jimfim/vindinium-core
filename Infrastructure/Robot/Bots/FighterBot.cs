using System.Linq;
using vindiniumcore.Infrastructure.Robot.Movement;
using vindiniumcore.Infrastructure.Robot.Tactics;

namespace vindiniumcore.Infrastructure.Robot.Bots
{
    class FighterBot : IBot
    {
        private readonly ITactic _tactic;
        private readonly IPathFinding _pathFinding;
        public string BotName => "Thunder Budddy";

        public FighterBot(ITactic tactic, IPathFinding pathFinding)
        {
            _tactic = tactic;
            _pathFinding = pathFinding;
        }

        public void Run(Server server)
        {
            while (server.Finished == false && server.Errored == false)
            {
                var destination = _tactic.NextDestination();
                var route = _pathFinding.GetShortestCompleteRouteToLocation(destination.Location);

                string direction = "Stay";
                if (route != null)
                {

                    direction = server.GetDirection(server.MyHero.Location,
                        route.Any() ? route.First().Location : null);
                }

                server.MoveHero(direction);
            }
        }
    }
}
