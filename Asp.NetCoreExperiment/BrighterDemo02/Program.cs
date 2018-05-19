using Paramore.Brighter;
using Paramore.Brighter.Logging;
using System;

namespace BrighterDemo02
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogProvider.For<Program>();

            var registry = new SubscriberRegistry();
            registry.Register<GreetingCommand, GreetingCommandHandler>();


            var builder = CommandProcessorBuilder.With()
                .Handlers(new HandlerConfiguration(
                    subscriberRegistry: registry,
                    handlerFactory: new SimpleHandlerFactory(logger)
                ))
                .DefaultPolicy()
                .NoTaskQueues()
                .RequestContextFactory(new InMemoryRequestContextFactory());

            var commandProcessor = builder.Build();
            commandProcessor.Send<GreetingCommand>(new GreetingCommand("GreetingCommand") {  });
        }
    }
    internal class SimpleHandlerFactory : IAmAHandlerFactory
    {
        ILog _log;
        public  SimpleHandlerFactory(ILog log)
        {
            _log = log;
        }
        public IHandleRequests Create(Type handlerType)
        {
            if (handlerType == typeof(GreetingCommandHandler))
            {
                _log.Info("type is GreetingCommandHandler");
                return new GreetingCommandHandler();
            }
            else
            {
                _log.Info("type is not GreetingCommandHandler");
                return null; // Ignore other handler types for demo
            }
        }

        public void Release(IHandleRequests handler)
        {
        }
    }
    public class GreetingCommand : IRequest
    {
        public GreetingCommand(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
    }
    public class GreetingCommandHandler : RequestHandler<GreetingCommand>
    {
        public override GreetingCommand Handle(GreetingCommand command)
        {
            Console.WriteLine("Hello {0}", command.Name);
            return base.Handle(command);
        }
    }
}
