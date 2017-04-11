using vindiniumcore.Infrastructure.Behaviors.Map;
using vindiniumcore.Infrastructure.Extensions;

namespace vindiniumcore.Infrastructure.Behaviors.Tactics
{
    public class SurvivalGoldRush : ITactic
    {
        private readonly Server _game;

        public SurvivalGoldRush(Server game)
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
