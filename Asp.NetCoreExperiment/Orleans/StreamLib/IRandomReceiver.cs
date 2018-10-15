using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace StreamLib
{

    public interface IRandomReceiver : IGrainWithGuidKey
    {
        Task Method1();
    }
    [ImplicitStreamSubscription("StreamLib")]
    public class ReceiverGrain : Grain, IRandomReceiver
    {


        public Task Method1()
        {
            Console.WriteLine("这里是Grain的一个类，IHelloWorld.Method1");

            var streamProvider = GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<string>(this.GetPrimaryKey(), "StreamLib");
            stream.OnNextAsync("ddd");

            return Task.CompletedTask;
        }

        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<string>(this.GetPrimaryKey(), "StreamLib");

            var subscriptionHandles = await stream.GetAllSubscriptionHandles();
            if (subscriptionHandles != null)
            {
                (subscriptionHandles as List<StreamSubscriptionHandle<string>>).ForEach(async x => await x.ResumeAsync(new AsyncObserver<string>()));
            }
        }
    }

    public class AsyncObserver<T> : IAsyncObserver<T>
    {
        public Task OnCompletedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }

        public Task OnNextAsync(T item, StreamSequenceToken token = null)
        {
            return Task.CompletedTask;
        }
    }

}