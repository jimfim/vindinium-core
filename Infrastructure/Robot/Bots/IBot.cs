namespace vindiniumcore.Infrastructure.Robot.Bots
{
	internal interface IBot
	{
		string BotName { get; }

		void Run(Server server);
	}
}