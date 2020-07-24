using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Concurrency;
using Orleans.Core;
using Orleans.Runtime;
using Orleans.Runtime.Services;
using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrainServices_Lib
{
    public interface IDataService : IGrainService
    {
        Task MyMethod();
    }


    [Reentrant]

    public class LightstreamerDataService : GrainService, IDataService
    {

        readonly IGrainFactory _grainFactory;
        readonly IServiceProvider _services;
        readonly ILoggerFactory _loggerFactory;

        ILogger<LightstreamerDataService> _logger;

        public LightstreamerDataService(IServiceProvider services, IGrainIdentity id, Silo silo, ILoggerFactory loggerFactory, IGrainFactory grainFactory) : base(id, silo, loggerFactory)
        {
            _grainFactory = grainFactory;
            _services = services;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<LightstreamerDataService>();
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
            _logger.LogInformation($"我的方法的日志：MyMethod");
            Console.WriteLine("我的方法：MyMethod");
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
}
