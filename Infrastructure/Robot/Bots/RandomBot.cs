using System;
using System.Threading.Tasks;
using vindiniumcore.Infrastructure.DTOs;

namespace vindiniumcore.Infrastructure.Robot.Bots
{
    class RandomBot : IBot
    {
        private readonly Server _server;

        public RandomBot(Server server)
        {
            _server = server;
        }

		public string BotName => "RandomBot";

        //starts everything
        public Task Run()
        {
            Console.Out.WriteLine("random bot running");
            Random random = new Random();
            while (_server.Finished == false && _server.Errored == false)
            {
                switch(random.Next(0, 6))
                {
                    case 0:
                        _server.MoveHero(Direction.East);
                        break;
                    case 1:
                        _server.MoveHero(Direction.North);
                        break;
                    case 2:
                        _server.MoveHero(Direction.South);
                        break;
                    case 3:
                        _server.MoveHero(Direction.Stay);
                        break;
                    case 4:
                        _server.MoveHero(Direction.West);
                        break;
                }

                Console.Out.WriteLine("completed turn " + _server.CurrentTurn);
            }

            if (_server.Errored)
            {
                Console.Out.WriteLine("error: " + _server.ErrorText);
            }

            Console.Out.WriteLine("random bot Finished");
        }
    }
}
