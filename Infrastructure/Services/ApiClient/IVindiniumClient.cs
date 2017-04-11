namespace vindiniumcore.Infrastructure.Services.ApiClient
{
    public interface IVindiniumClient
    {
        bool MoveHero(string direction);

        bool CreateGame();
    }
}
