using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Concurrency;
using Orleans.Core;
using Orleans.Hosting;
using Orleans.Runtime;
using Orleans.Runtime.Services;
using Orleans.Services;
using System;
using System.Threading.Tasks;

namespace GrainServices_SiloHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new SiloHostBuilder()
                .AddGrainService<LightstreamerDataService>()  // Register GrainService
                .ConfigureServices(s =>
                {
                    // Register Client of GrainService
                    s.AddSingleton<IDataServiceClient, DataServiceClient>();
                });
        }
    }


    public interface IDataService : IGrainService
    {
        Task MyMethod();
    }


    [Reentrant]

    public class LightstreamerDataService : GrainService, IDataService
    {

        readonly IGrainFactory _grainFactory;

        public LightstreamerDataService(IServiceProvider services, IGrainIdentity id, Silo silo, ILoggerFactory loggerFactory, IGrainFactory grainFactory) : base(id, silo, loggerFactory)
        {
            _grainFactory = grainFactory;
        }

        public override Task Init(IServiceProvider serviceProvider)
        {
            return base.Init(serviceProvider);
        }

        public override async Task Start()
        {
            await base.Start();
        }

        public override Task Stop()
        {
            return base.Stop();
        }

        public Task MyMethod()
        {
            Console.WriteLine("MyMethod");
            return Task.CompletedTask;
        }
    }


    public interface IDataServiceClient : IGrainServiceClient<IDataService>, IDataService
    {
    }

    public class DataServiceClient : GrainServiceClient<IDataService>, IDataServiceClient
    {

        public DataServiceClient(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task MyMethod() => GrainService.MyMethod();
    }

    public interface INormalGrain
    {
    }
    public class NormalGrainState
    {
    }
    public class MyNormalGrain : Grain<NormalGrainState>, INormalGrain
    {

        readonly IDataServiceClient DataServiceClient;

        public MyNormalGrain(IGrainActivationContext grainActivationContext, IDataServiceClient dataServiceClient)
        {
            DataServiceClient = dataServiceClient;
        }
    }
}
