using System;
using System.Diagnostics;
using Autofac;
using vindiniumcore.Infrastructure.Bots;

namespace vindiniumcore.Infrastructure
{
    class Startup : IStartable
    {
        private readonly Server _server;

        public Startup(Server server)
        {
            _server = server;
        }

        public void Start()
        {
            _server.CreateGame();
            if (_server.Errored == false)
            {
                //opens up a webpage so you can view the game, doing it async so we dont time out
                Process.Start("cmd", "/C start " + _server.ViewUrl);
            }

            var bot = new Robot(_server);
            bot.Run();
            Console.ReadLine();
        }
    }
}
