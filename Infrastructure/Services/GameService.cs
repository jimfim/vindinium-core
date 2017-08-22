namespace vindiniumcore.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private static GameDetails Instance { get; set; }

        public void UpdateGame(GameDetails gameDetails)
        {
            Instance = gameDetails;
        }

        public GameDetails GetGame()
        {
            return Instance;
        }
    }

    public interface IGameService
    {
        GameDetails GetGame();

        void UpdateGame(GameDetails gameDetails);
    }
}
