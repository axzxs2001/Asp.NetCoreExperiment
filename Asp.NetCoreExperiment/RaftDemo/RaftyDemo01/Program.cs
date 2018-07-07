using Microsoft.Extensions.Logging;
using Rafty.Concensus.Node;
using Rafty.Concensus.Peers;
using Rafty.FiniteStateMachine;
using Rafty.Infrastructure;
using Rafty.Log;
using System;
using System.Collections.Generic;

namespace RaftyDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new InMemoryLog();
       
            var fsm = new InMemoryStateMachine();
            var settings = new InMemorySettings(1000, 3500, 50, 5000);

            var _peers = new List<IPeer>();
            var peersProvider = new InMemoryPeersProvider(_peers);
            var node = new Node(fsm, log, settings, peersProvider,null);
            node.Start(new NodeId("gsw"));
        }
    }
}
