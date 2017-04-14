using vindiniumcore.Infrastructure.Extensions;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Robot.Tactics
{
    public class Strategic : ITactic
    {
        private readonly Server _game;

        public Strategic(Server game)
        {
            _game = game;
        }

        public IMapNode NextDestination()
        {
            var hero = _game.MyHero as HeroNode;
            if (hero == null || _game.GetClosestChest() == null)
            {
                return _game.GetClosestTavern();
            }

            if ((hero.Life < 30) || (hero.Life < 90 && _game.GetClosestTavern().MovementCost < 2))
            {
                return _game.GetClosestTavern();
            }

            return _game.GetClosestChest();
        }
    }
}
