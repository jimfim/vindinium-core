namespace vindiniumcore.Infrastructure.Services.ApiClient
{
    public interface IVindiniumClient
    {
        void MoveHero(string direction);

        void CreateGame();
    }
}
