using System;
using System.Diagnostics;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using vindinium.Infrastructure.Bots;
using vindinium.Infrastructure.Mappings;
using vindinium.Infrastructure.Services.ApiClient;
using vindiniumcore.Infrastructure.Services.ApiClient;

namespace vindinium
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var provider = ConfigureServices();

            IVindiniumClient service = provider.GetService<IVindiniumClient>();
            service.CreateGame();

            string serverUrl = args.Length == 4 ? args[3] : "http://vindinium.org";


            Server server = new Server(args[0], args[1] != "arena", uint.Parse(args[2]), serverUrl, args[4], mapper);
            server.CreateGame();
            if (server.Errored == false)
            {
                //opens up a webpage so you can view the game, doing it async so we dont time out
                Process.Start("cmd", "/C start "+server.ViewUrl);
            }

            var bot = new Robot(server);
            bot.Run();
    
            Console.Out.WriteLine("done");
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IVindiniumClient, VindiniumClient>();
            services.AddSingleton<IBot, Robot>();
            services.AddAutoMapper();

            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }
    }
}