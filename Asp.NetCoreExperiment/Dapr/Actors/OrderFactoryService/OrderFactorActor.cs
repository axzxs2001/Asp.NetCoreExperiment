using Dapr.Actors.Runtime;
using IOrderFactoryActory.Interfaces;

namespace OrderFactoryService
{
    public class OrderFactorActor : Actor, IOrderFactoryActory.Interfaces.IOrderFactoryActory, IRemindable
    {
        public OrderFactorActor(ActorHost host) : base(host)
        { 
        }

        protected override Task OnActivateAsync()
        {
            Console.WriteLine($"Activating actor id:{this.Id}");
            return Task.CompletedTask;
        }

        protected override Task OnDeactivateAsync()
        {
            Console.WriteLine($"Deactivating actor id:{this.Id}");
            return Task.CompletedTask;
        }     

        public async Task RegisterReminder()
        {
            await this.RegisterReminderAsync("MyReminder",
            null,
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(5));
        }

        public Task UnregisterReminder()
        {
            Console.WriteLine("Unregistering MyReminder");
            return this.UnregisterReminderAsync("MyReminder");
        }

        public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
        {
            Console.WriteLine("ReceiveReminderAsync is called!");
            return Task.CompletedTask;
        }

        public Task RegisterTimer()
        {
            return this.RegisterTimerAsync(
                "MyTimer",// The name of the timer
                nameof(this.OnTimerCallBack),// Timer callback
                null,// User state passed to OnTimerCallback()
                TimeSpan.FromSeconds(5),// Time to delay before the async callback is first invoked
                TimeSpan.FromSeconds(5));// Time interval between invocations of the async callback
        }
        private Task OnTimerCallBack(byte[] data)
        {
            Console.WriteLine("OnTimerCallBack is called!");
            return Task.CompletedTask;
        }

        public Task UnregisterTimer()
        {
            Console.WriteLine("Unregistering MyTimer...");
            return this.UnregisterTimerAsync("MyTimer");
        }
    }
}
