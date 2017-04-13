using System;
using System.Diagnostics;
using Autofac;
using vindiniumcore.Infrastructure.Bots;

namespace vindiniumcore.Infrastructure
{
    class Startup : IStartable
    {
        private readonly Server _server;
        private readonly ILifetimeScope _lifetimeScope;

        public Startup(Server server,ILifetimeScope lifetimeScope)
        {
            _server = server;
            _lifetimeScope = lifetimeScope;
        }

        public void Start()
        {
            _server.CreateGame();
            if (_server.Errored == false)
            {
                //opens up a webpage so you can view the game, doing it async so we dont time out
                Process.Start("cmd", "/C start " + _server.ViewUrl);
            }

            _lifetimeScope.Resolve<IBot>().Run(_server);
        }
    }
}
