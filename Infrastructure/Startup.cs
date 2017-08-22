using System.Diagnostics;
using Autofac;
using vindiniumcore.Infrastructure.Robot.Bots;
using vindiniumcore.Infrastructure.Services.ApiClient;

namespace vindiniumcore.Infrastructure
{
    class Startup : IStartable
    {
        private readonly Server _server;
        private readonly IVindiniumClient _vindiniumClient;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly VindiniumSettings _vindiniumSettings;

        public Startup(IVindiniumClient vindiniumClient,ILifetimeScope lifetimeScope,VindiniumSettings vindiniumSettings, Server server)
        {
            _vindiniumClient = vindiniumClient;
            _lifetimeScope = lifetimeScope;
            _vindiniumSettings = vindiniumSettings;
            _server = server;
        }

        public async void Start()
        {
            do
            {
                var game = await _vindiniumClient.CreateGame();
                if (game.Errored == false && _vindiniumSettings.TrainingMode)
                {
                    //opens up a webpage so you can view the game, doing it async so we dont time out
                    Process.Start("cmd", "/C start " + _server.ViewUrl);
                }

                _lifetimeScope.Resolve<IBot>().Run(game);
            } while (!_vindiniumSettings.TrainingMode);
        }
    }
}
