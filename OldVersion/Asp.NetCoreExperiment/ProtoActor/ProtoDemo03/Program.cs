using Proto;
using System;
using System.Threading.Tasks;

namespace ProtoDemo03
{
    class Program
    {
        static void Main(string[] args)
        {
            var pid = Actor.Spawn(Actor.FromProducer(() => new LightBulb()));

            var result0 = pid.RequestAsync<object>(new Touch()).GetAwaiter().GetResult();
            Console.WriteLine(result0);


            var result1 = pid.RequestAsync<object>(new PressSwitch()).GetAwaiter().GetResult();
            Console.WriteLine(result1);

            var result2 = pid.RequestAsync<object>(new PressSwitch()).GetAwaiter().GetResult();
            Console.WriteLine(result2);
            Console.ReadLine();
        }
    }

    public class LightBulb : IActor
    {
        private readonly Behavior _behavior;

        public LightBulb()
        {
            _behavior = new Behavior();
            _behavior.Become(Off);
        }
        public Task ReceiveAsync(IContext context)
        {
            // any "global" message handling here
            switch (context.Message)
            {
                case HitWithHammer _:
                    context.Respond("Smashed!");
                    _behavior.Become(Smashed);
                    return Actor.Done;
            }
            // if not handled, use behavior specific
            return _behavior.ReceiveAsync(context);
        }

        private Task Off(IContext context)
        {
            switch (context.Message)
            {
                case PressSwitch _:
                    context.Respond("Turning on");
                    _behavior.Become(On);
                    break;
                case Touch _:
                    context.Respond("Cold");
                    break;
            }

            return Actor.Done;
        }

        private Task On(IContext context)
        {
            switch (context.Message)
            {
                case PressSwitch _:
                    context.Respond("Turning off");
                    _behavior.Become(Off);
                    break;
                case Touch _:
                    context.Respond("Hot!");
                    break;
            }

            return Actor.Done;
        }
        private Task Smashed(IContext context)
        {
            switch (context.Message)
            {
                case PressSwitch _:
                    context.Respond(""); // nothing happens!
                    break;
                case Touch _:
                    context.Respond("Owwww!");
                    break;
                case ReplaceBulb _:
                    _behavior.Become(Off);
                    break;
            }

            return Actor.Done;
        }
    }

    public class PressSwitch
    {

    }
    public class Touch
    {

    }
    public class ReplaceBulb
    {

    }
    public class HitWithHammer
    {

    }

}
