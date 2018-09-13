using Orleans;
using System;
using System.Threading.Tasks;

namespace Orleans005_Interceptors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    public interface IMyFilteredGrain
    {

    }
    public class MyFilteredGrain : Grain, IMyFilteredGrain, IIncomingGrainCallFilter, IOutgoingGrainCallFilter
    {
        public async Task Invoke(IIncomingGrainCallContext context)
        {
            await context.Invoke();

            // Change the result of the call from 7 to 38.
            if (string.Equals(context.InterfaceMethod.Name, nameof(this.GetFavoriteNumber)))
            {
                context.Result = 38;
            }
        }

        public Task<int> GetFavoriteNumber() => Task.FromResult(7);

        public async Task Invoke(IOutgoingGrainCallContext context)
        {
            await context.Invoke();

            // Change the result of the call from 7 to 38.
            if (string.Equals(context.InterfaceMethod.Name, nameof(this.GetFavoriteNumber)))
            {
                context.Result = 58;
            }
        }
    }
}
