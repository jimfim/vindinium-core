﻿using vindiniumcore.Infrastructure.Behaviors.Map;
using vindiniumcore.Infrastructure.Extensions;

namespace vindiniumcore.Infrastructure.Behaviors.Tactics
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
