using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace StreamLib
{

    public interface IRandomReceiver : IGrainWithGuidKey
    {
        Task Method1(string message);
    }

    public class ReceiverGrain : Grain, IRandomReceiver
    {

        IAsyncStream<string> _stream;
        public async Task Method1(string message)
        {
            await _stream.OnNextAsync(message);
            Console.WriteLine("这里是Grain的一个类，IHelloWorld.Method1");
        }

        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("SMSProvider");
            _stream = streamProvider.GetStream<string>(this.GetPrimaryKey(), "StreamLib");
        
            await base.OnActivateAsync();

        }
    }



    public class AsyncObserver : IAsyncObserver<string>
    {
        public Task OnCompletedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }

        public Task OnNextAsync(string item, StreamSequenceToken token = null)
        {
            Console.WriteLine("AsyncObserver Itme:" + item);
            return Task.CompletedTask;
        }
    }


    public class AsyncObservable : IAsyncObservable<string>
    {
        public Task<StreamSubscriptionHandle<string>> SubscribeAsync(IAsyncObserver<string> observer)
        {
            throw new NotImplementedException();
        }

        public Task<StreamSubscriptionHandle<string>> SubscribeAsync(IAsyncObserver<string> observer, StreamSequenceToken token, StreamFilterPredicate filterFunc = null, object filterData = null)
        {
            throw new NotImplementedException();
        }
    }

}