
using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Observer_Lib
{
    public interface IHello : IGrainWithGuidKey
    {    
        Task SendUpdateMessage(string message);
        Task Subscribe(IChat observer);
        Task UnSubscribe(IChat observer);
    }
    public class HelloGrain : Grain, IHello
    {
        private GrainObserverManager<IChat> _subsManager;

        public override async Task OnActivateAsync()
        {        
            _subsManager = new GrainObserverManager<IChat>();
            _subsManager.ExpirationDuration = TimeSpan.FromSeconds(100);        
            await base.OnActivateAsync();
        }

        public Task Subscribe(IChat observer)
        {
            if (!_subsManager.IsSubscribed(observer))
            {
                _subsManager.Subscribe(observer);
            }
            return Task.CompletedTask;
        }
        
        public Task UnSubscribe(IChat observer)
        {
            if (_subsManager.IsSubscribed(observer))
            {
                _subsManager.Unsubscribe(observer);
            }
            return Task.CompletedTask;
        }
        public Task SendUpdateMessage(string message)
        {
            Console.WriteLine($"服务端收到消息：{message}");
            _subsManager.Notify(s => s.ReceiveMessage(message));
            return Task.CompletedTask;
        }
     
    
    }
}
