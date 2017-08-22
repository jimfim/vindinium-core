using vindiniumcore.Infrastructure.Extensions;
using vindiniumcore.Infrastructure.Map;
using vindiniumcore.Infrastructure.Services;

namespace vindiniumcore.Infrastructure.Robot.Tactics
{
    public class Strategic : ITactic
    {
        private readonly IGameService _gameService;

        public Strategic(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IMapNode NextDestination()
        {
            var game = _gameService.GetGame();
            var hero = game.MyHero as HeroNode;
            if (hero == null || game.GetClosestChest() == null)
            {
                return game.GetClosestTavern();
            }

            if ((hero.Life < 30) || (hero.Life < 90 && game.GetClosestTavern().MovementCost < 2))
            {
                return game.GetClosestTavern();
            }

            return game.GetClosestChest();
        }
    }
}
