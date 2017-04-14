using vindiniumcore.Infrastructure.Extensions;
using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Robot.Tactics
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
