using vindiniumcore.Infrastructure.Behaviors.Extensions;
using vindiniumcore.Infrastructure.Behaviors.Map;

namespace vindiniumcore.Infrastructure.Behaviors.Tactics
{
    /// <summary>
    /// Charges for closest un-owned goldmine
    /// </summary>
    public class DumbGoldRush : ITactic
    {
        private readonly Server _game;

        public DumbGoldRush(Server game)
        {
            this._game = game;
        }

        public IMapNode NextDestination()
        {
            return this._game.GetClosestChest();
        }
    }
}
