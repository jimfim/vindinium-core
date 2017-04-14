using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Newtonsoft.Json;
using vindiniumcore.Infrastructure.DTOs;
using vindiniumcore.Infrastructure.Robot.Bots;
using vindiniumcore.Infrastructure.Services.ApiClient;

namespace vindiniumcore.Infrastructure
{
    class Startup : IStartable
    {
        private readonly Server _server;
        private readonly IVindiniumClient _vindiniumClient;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly VindiniumSettings _vindiniumSettings;
        private readonly IMapper _mapper;

        public Startup(IVindiniumClient vindiniumClient,ILifetimeScope lifetimeScope,VindiniumSettings vindiniumSettings, IMapper mapper)
        {
            _vindiniumClient = vindiniumClient;
            _lifetimeScope = lifetimeScope;
            _vindiniumSettings = vindiniumSettings;
            _mapper = mapper;
        }

        public async void Start()
        {
            var test = "{\"game\":{\"id\":\"s2xh3aig\",\"turn\":1100,\"maxTurns\":1200,\"heroes\":[{\"id\":1,\"name\":\"vjousse\",\"userId\":\"j07ws669\",\"elo\":1200,\"pos\":{\"x\":5,\"y\":6},\"life\":60,\"gold\":0,\"mineCount\":0,\"spawnPos\":{\"x\":5,\"y\":6},\"crashed\":true},{\"id\":2,\"name\":\"vjousse\",\"userId\":\"j07ws669\",\"elo\":1200,\"pos\":{\"x\":12,\"y\":6},\"life\":100,\"gold\":0,\"mineCount\":0,\"spawnPos\":{\"x\":12,\"y\":6},\"crashed\":true},{\"id\":3,\"name\":\"vjousse\",\"userId\":\"j07ws669\",\"elo\":1200,\"pos\":{\"x\":12,\"y\":11},\"life\":80,\"gold\":0,\"mineCount\":0,\"spawnPos\":{\"x\":12,\"y\":11},\"crashed\":true},{\"id\":4,\"name\":\"vjousse\",\"userId\":\"j07ws669\",\"elo\":1200,\"pos\":{\"x\":4,\"y\":8},\"lastDir\":\"South\",\"life\":38,\"gold\":1078,\"mineCount\":6,\"spawnPos\":{\"x\":5,\"y\":11},\"crashed\":false}],\"board\":{\"size\":18,\"tiles\":\"##############        ############################        ##############################    ##############################$4    $4############################  @4    ########################  @1##    ##    ####################  []        []  ##################        ####        ####################  $4####$4  ########################  $4####$4  ####################        ####        ##################  []        []  ####################  @2##    ##@3  ########################        ############################$-    $-##############################    ##############################        ############################        ##############\"},\"finished\":true},\"hero\":{\"id\":4,\"name\":\"vjousse\",\"userId\":\"j07ws669\",\"elo\":1200,\"pos\":{\"x\":4,\"y\":8},\"lastDir\":\"South\",\"life\":38,\"gold\":1078,\"mineCount\":6,\"spawnPos\":{\"x\":5,\"y\":11},\"crashed\":false},\"token\":\"lte0\",\"viewUrl\":\"http://localhost:9000/s2xh3aig\",\"playUrl\":\"http://localhost:9000/api/s2xh3aig/lte0/play\"}";
            var narf = JsonConvert.DeserializeObject<GameResponse>(test);
            var output = _mapper.Map<GameDetails>(narf);
            //do
            //{
            //    var game = await _vindiniumClient.CreateGame();
            //    if (game.Errored == false && _vindiniumSettings.TrainingMode)
            //    {
            //        //opens up a webpage so you can view the game, doing it async so we dont time out
            //        Process.Start("cmd", "/C start " + _server.ViewUrl);
            //    }

            //    _lifetimeScope.Resolve<IBot>().Run(_server);
            //} while (!_vindiniumSettings.TrainingMode);
        }
    }
}
