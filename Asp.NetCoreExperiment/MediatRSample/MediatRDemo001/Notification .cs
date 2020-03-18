using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRDemo001
{
    /// <summary>
    /// 通知主体
    /// </summary>
    public class Ping : INotification { }


    /// <summary>
    /// 相当订阅者1
    /// </summary>
    public class Pong1 : INotificationHandler<Ping>
    {
        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Pong 1");
            return Task.CompletedTask;
        }
    }
    /// <summary>
    /// 相当订阅者2
    /// </summary>
    public class Pong2 : INotificationHandler<Ping>
    {
        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Pong 2");
            return Task.CompletedTask;
        }
    }
}
