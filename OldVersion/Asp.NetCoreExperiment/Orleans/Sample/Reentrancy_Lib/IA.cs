
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Reentrancy_Lib
{
    public interface IA : IGrainWithIntegerKey
    {
        Task<bool> Go(int I);
    }

    public class A : Grain, IA
    {
        public async Task<bool> Go(int I)
        {
            Console.WriteLine($"A中Go方法，I={I}");
            if (I == 0)
            {
                return true;
            }

            var grain = this.GrainFactory.GetGrain<IB>(0);
            return await grain.Go(I - 1);
        }


    }
}
