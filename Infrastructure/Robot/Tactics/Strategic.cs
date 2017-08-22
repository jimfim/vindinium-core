using vindiniumcore.Infrastructure.Map;
using vindiniumcore.Infrastructure.Services;

namespace vindiniumcore.Infrastructure.Robot.Tactics
{
    public class GoldRush : ITactic
    {
        private readonly IGameService _gameService;

        public GoldRush(IGameService gameService)
        {
            _gameService = gameService;
        }
        public IMapNode NextDestination()
        {

            ////b.avoidBarBrawlsWithAllies, // Don't get stuck in a loop hurting allies
            ////b.doTelefrag,               // Telefrag someone if we can
            ////b.ratherDie,                // Suicide rather than give our mines to an opponent
            ////b.grabTavern,               // Interpose between enemy and tavern (short distance)
            ////b.goHunt,                   // Try to kill a nearby enemy
            ////b.snatchMine,               // Conquer a mine, but only if no enemy is near
            ////b.interceptEnemy,           // Try to intercept an enemy on his way to the tavern
            ////b.drinkIfEnemyIsNear,       // Drink if enemy is near
            ////b.assuredVictory,           // If we are likely to win, hang out at the tavern (thanks petbot!)
            ////b.doDrink,                  // Drink if we need to
            ////b.standAndFight,            // Deal as much damage as possible if we're in a fight we can't win
            ////b.goAfterTheRichKid,        // If someone has 50% of the mines, go attack him
            ////b.goMining,                 // Capture the nearest mine
            ////b.goToTavern,               // Go to the nearest tavern
            ////b.maybeSuicide,             // Can't do anything. Suicide if we don't have too many mines.
            ////b.bumRush,                  // Can't even suicide. Bum Rush the closest enemy.

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
