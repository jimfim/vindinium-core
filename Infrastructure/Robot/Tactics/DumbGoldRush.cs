using vindiniumcore.Infrastructure.Map;
using vindiniumcore.Infrastructure.Services;

namespace vindiniumcore.Infrastructure.Robot.Tactics
{
    /// <summary>
    /// Charges for closest un-owned goldmine
    /// </summary>
    public class DumbGoldRush : ITactic
    {
        private readonly IGameService _gameService;

        public DumbGoldRush(IGameService gameService)
        {
            _gameService = gameService;
        }
        public IMapNode NextDestination()
        {
            return _gameService.GetGame().GetClosestChest();
        }
    }
}
