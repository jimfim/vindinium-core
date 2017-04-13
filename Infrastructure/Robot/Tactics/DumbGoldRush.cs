using vindiniumcore.Infrastructure.Behaviors.Map;
using vindiniumcore.Infrastructure.Extensions;

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
            _game = game;
        }

        public IMapNode NextDestination()
        {
            return _game.GetClosestChest();
        }
    }
}
