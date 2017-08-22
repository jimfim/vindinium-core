using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using vindiniumcore.Infrastructure.DTOs;

namespace vindiniumcore.Infrastructure.Services.ApiClient
{
    public class VindiniumClient : IVindiniumClient
    {
        private readonly VindiniumSettings _vindiniumSettings;
        private readonly IMapper _mapper;
        private readonly HttpClient _client;

        public VindiniumClient(VindiniumSettings vindiniumSettings, IMapper mapper)
        {
            _vindiniumSettings = vindiniumSettings;
            _mapper = mapper;
            _client = new HttpClient();
        }

        public async Task<GameDetails> MoveHero(string direction)
        {
            var myParameters = "key=" + _vindiniumSettings.Key + "&dir=" + direction;
           // var result = await _client.PostAsync(_vindiniumSettings.ServerUrl + "?" + myParameters, new StringContent(string.Empty)).ContinueWith(DeserializeResponse);
            return null;
        }

        public async Task<GameDetails> CreateGame()
        {
            string uri = string.Empty;
            if (_vindiniumSettings.TrainingMode)
            {
                uri = _vindiniumSettings.ServerUrl + "api/training";
            }
            else
            {
                uri = _vindiniumSettings.ServerUrl + "api/arena";
            }
            var myParameters = "key=" + _vindiniumSettings.Key;
            if (_vindiniumSettings.TrainingMode)
            {
                myParameters += "&turns=" + _vindiniumSettings.Turns;
            }
            if (_vindiniumSettings.Map != null)
            {
                myParameters += "&map=" + _vindiniumSettings.Map;
            }

            try
            {
                var response =  _client.PostAsync(uri + "?" + myParameters, new StringContent(string.Empty)).Result;
                return DeserializeResponse(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        private GameDetails DeserializeResponse(HttpResponseMessage x)
        {
            var apiResponse = x.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<GameResponse>(apiResponse.Result);
            var test = _mapper.Map<GameDetails>(response);
            return test;
        }
    }
}