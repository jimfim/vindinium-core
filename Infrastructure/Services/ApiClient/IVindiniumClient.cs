using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vindinium.Infrastructure.Services.ApiClient
{
    public interface IVindiniumClient
    {
        bool MoveHero(string direction);

        bool CreateGame();
    }
}
