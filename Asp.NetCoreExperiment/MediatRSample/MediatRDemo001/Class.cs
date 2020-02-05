using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRDemo001
{
    public class Class
    {
    }
    public class Ping : INotification { }
    public class Pong1 : INotificationHandler<Ping>
    {
        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Pong 1");
            return Task.CompletedTask;
        }
    }

    public class Pong2 : INotificationHandler<Ping>
    {
        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Pong 2");
            return Task.CompletedTask;
        }
    }
}
