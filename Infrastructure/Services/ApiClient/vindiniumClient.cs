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
            var result = await _client.PostAsync(_vindiniumSettings.ServerUrl + "?" + myParameters, new StringContent(string.Empty)).ContinueWith(DeserializeResponse);
            return result.Result;
        }

        public async Task<GameDetails> CreateGame()
        {
            var response = await _client.PostAsync(_vindiniumSettings.ServerUrl, new StringContent(string.Empty)).ContinueWith(DeserializeResponse);
            return response.Result;

        }

        private async Task<GameDetails> DeserializeResponse(Task<HttpResponseMessage> x)
        {
            var apiResponse = await x.Result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<GameResponse>(apiResponse);
            return _mapper.Map<GameDetails>(response);
        }
    }
}