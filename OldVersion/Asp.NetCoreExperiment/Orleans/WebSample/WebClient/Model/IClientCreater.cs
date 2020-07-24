using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Model
{
    public interface IClientCreater
    {
        Task<IClusterClient> CreateClient();
    }
}
