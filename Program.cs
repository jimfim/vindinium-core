﻿using System;
using System.Diagnostics;
using System.Threading;
using AutoMapper;
using vindinium.Infrastructure.Bots;
using vindinium.Infrastructure.Mappings;

namespace vindinium
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverUrl = args.Length == 4 ? args[3] : "http://vindinium.org";

            //create the server stuff, when not in training mode, it doesnt matter
            //what you use as the number of turns


            var config = new MapperConfiguration(cfg => { cfg.AddProfile<HeroMapping>(); });
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();


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
    }
}