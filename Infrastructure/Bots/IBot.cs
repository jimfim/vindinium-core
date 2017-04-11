namespace vindiniumcore.Infrastructure.Bots
{
	internal interface IBot
	{
		string BotName { get; }

		void Run();
	}
}