using Autofac;
using vindiniumcore.Infrastructure.Robot.Bots;

namespace vindiniumcore.Infrastructure
{
    public class Startup : IStartable
    {
        private readonly IBot _bot;

        public Startup(IBot bot)
        {
            _bot = bot;
        }

        public void Start()
        {
            //opens up a webpage so you can view the game, doing it async so we dont time out
            //Process.Start("cmd", "/C start " + _server.ViewUrl);
            _bot.Run();
        }
    }
}
