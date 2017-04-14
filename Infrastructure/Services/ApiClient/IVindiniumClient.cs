using System.Threading.Tasks;

namespace vindiniumcore.Infrastructure.Services.ApiClient
{
    public interface IVindiniumClient
    {
        Task<GameDetails> MoveHero(string direction);

        Task<GameDetails> CreateGame();
    }
}
