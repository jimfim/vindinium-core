using System;
using System.Threading;
using vindiniumcore.Infrastructure.DTOs;

namespace vindiniumcore.Infrastructure.Bots
{
    class RandomBot : IBot
    {
		public string BotName => "RandomBot";

        private readonly Server server;

        public RandomBot(Server server)
        {
            this.server = server;
        }

        //starts everything
        public void Run()
        {
            Console.Out.WriteLine("random bot running");

            server.CreateGame();

            if (server.Errored == false)
            {
                //opens up a webpage so you can view the game, doing it async so we dont time out
                new Thread(delegate()
                {
                    System.Diagnostics.Process.Start(server.ViewUrl);
                }).Start();
            }
            
            Random random = new Random();
            while (server.Finished == false && server.Errored == false)
            {
                switch(random.Next(0, 6))
                {
                    case 0:
                        server.MoveHero(Direction.East);
                        break;
                    case 1:
                        server.MoveHero(Direction.North);
                        break;
                    case 2:
                        server.MoveHero(Direction.South);
                        break;
                    case 3:
                        server.MoveHero(Direction.Stay);
                        break;
                    case 4:
                        server.MoveHero(Direction.West);
                        break;
                }

                Console.Out.WriteLine("completed turn " + server.CurrentTurn);
            }

            if (server.Errored)
            {
                Console.Out.WriteLine("error: " + server.ErrorText);
            }

            Console.Out.WriteLine("random bot Finished");
        }


    }
}
