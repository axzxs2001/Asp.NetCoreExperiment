using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Concurrency;
using Orleans.Core;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Runtime.Services;
using Orleans.Services;
using System;
using System.Threading.Tasks;

namespace GrainServices_Lib
{


    public interface INormalGrain : IGrainWithGuidKey
    {
        Task Method1();
    }
    [Serializable]
    public class NormalGrainState
    {
        public string StateMessage { get; set; }
    }
    [StorageProvider(ProviderName = "OrleansStorage")]
    public class MyNormalGrain : Grain<NormalGrainState>, INormalGrain
    {

        readonly IDataServiceClient _dataServiceClient;

        public MyNormalGrain(IGrainActivationContext grainActivationContext, IDataServiceClient dataServiceClient)
        {
            _dataServiceClient = dataServiceClient;
        }

        public Task Method1()
        {
            return _dataServiceClient.MyMethod();
        }
        protected override Task WriteStateAsync()
        {
            State.StateMessage = DateTime.Now + " 状态信息！";
            return base.WriteStateAsync();
        }
    }
}
