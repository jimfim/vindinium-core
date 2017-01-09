using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vindinium.Infrastructure.Services.ApiClient;

namespace vindiniumcore.Infrastructure.Services.ApiClient
{
    public class VindiniumClient : IVindiniumClient
    {
        public bool MoveHero(string direction)
        {
            return true;
        }

        public bool CreateGame()
        {
            return true;
        }
    }
}
