using System;
using vindiniumcore.Infrastructure.DTOs;

namespace vindiniumcore.Infrastructure.Robot.Bots
{
    class RandomBot : IBot
    {
		public string BotName => "RandomBot";

        //starts everything
        public void Run(Server server)
        {
            Console.Out.WriteLine("random bot running");
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
