


using Orleans;
using Orleans.Concurrency;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Persistence_Lib
{
    public interface IHelloGrain : IGrainWithGuidKey
    {
        Task Write();
        Task Read();
    }
    [Serializable]
    public class HelloGrainState
    {    
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    [StorageProvider(ProviderName = "OrleansStorage")]
    public class HelloGrain : Grain<HelloGrainState>, IHelloGrain
    {

        public async Task Write()
        {
            State.Name = "gsw";
            State.Price = 1.23m;
            await base.WriteStateAsync(); await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine($"写入：{Newtonsoft.Json.JsonConvert.SerializeObject(State)}");
        }
        public async Task Read()
        {
            await base.ReadStateAsync();
            Console.WriteLine($"读出：{Newtonsoft.Json.JsonConvert.SerializeObject(State)}");
        }
    }
}
