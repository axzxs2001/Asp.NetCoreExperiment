using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;
namespace StreamLib
{

    public interface IReceiver : IGrainWithStringKey
    {
        Task Method1(Message message);
        Task<Guid> GetGuid();
    }
    [Serializable]
    public class ReceiverGrain : Grain, IReceiver
    {

        IAsyncStream<Message> _stream;
        public async Task Method1(Message message)
        {
            await _stream.OnNextAsync(message);
            Console.WriteLine("这里是Grain的一个类，IHelloWorld.Method1");
        }

        public async Task<Guid> GetGuid()
        {
            await _stream.OnNextAsync(new Message() { Content = "无" });
            return _stream.Guid;
        }

        public override async Task OnActivateAsync()
        {

            var streamProvider = GetStreamProvider("SMSProvider");
            _stream = streamProvider.GetStream<Message>(Guid.NewGuid(), "StreamLib");

            await base.OnActivateAsync();

        }
    }

    [Serializable]
    public class AsyncObserver : IAsyncObserver<Message>
    {
        public Task OnCompletedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }

        public Task OnNextAsync(Message item, StreamSequenceToken token = null)
        {
            Console.WriteLine("AsyncObserver Itme:" + item.Content);
            return Task.CompletedTask;
        }
    }

    [Serializable]
    public class Message
    {
        public string Content { get; set; }
    }
}