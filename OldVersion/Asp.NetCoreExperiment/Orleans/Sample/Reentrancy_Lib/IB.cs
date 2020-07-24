
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Reentrancy_Lib
{
    public interface IB : IGrainWithIntegerKey
    {
        Task<bool> Go(int I);
    }

    public class B : Grain, IB
    {
        public async Task<bool> Go(int I)
        {
            Console.WriteLine($"B中Go方法，I={I}");
            if (I == 0)
            {
                return false;
            }

            var grain = this.GrainFactory.GetGrain<IA>(0);
            return await grain.Go(I - 1);            
        }
    }
}
