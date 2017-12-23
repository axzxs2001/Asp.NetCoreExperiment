using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo01
{
    public class StreamingHub : Hub
    {
        public void SendStreamInit()
        {            //开启客户端订阅
            Clients.All.InvokeAsync("streamStarted");
        }
        //被订阅的消息
        public IObservable<string> StartStreaming()
        {
            return Observable.Create(
          async (IObserver<string> observer) =>
          {
              for (int i = 0; i < 10; i++)
              {
                  observer.OnNext($"发送内容......{i}"); await Task.Delay(1000);
              }
          });
        }
    }

    internal class Observable
    {
        internal static IObservable<string> Create(Func<IObserver<string>, Task> p)
        {
            throw new NotImplementedException();
        }
    }
}
