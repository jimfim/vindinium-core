namespace vindiniumcore.Infrastructure.Robot.Bots
{
	public interface IBot
	{
		string BotName { get; }

		void Run();
	}
}