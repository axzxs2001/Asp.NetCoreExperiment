using Dapr.Actors.Runtime;
using IOrderFactoryActory.Interfaces;

namespace OrderFactoryService
{
    //public class OrderFactorActor : Actor, IOrderFactoryActory.Interfaces.IOrderFactoryActory, IRemindable
    //{
    //    readonly string _id;
    //    public OrderFactorActor(ActorHost host) : base(host)
    //    {
    //        _id = host.Id.GetId();
    //    }

    //    public Task<decimal> GetOrderAmountAsync(Order order)
    //    {
    //        Task.Delay(3000).Wait();
    //        var discount = 1.0f;
    //        switch (order.OrderType)
    //        {
    //            case "A":
    //                discount = 0.7f;
    //                break;
    //            case "B":
    //                discount = 0.8f;
    //                break;
    //            default:
    //                discount = 1.0f;
    //                break;
    //        }
    //        switch (order.Quantity)
    //        {
    //            case 10:
    //                return Task.FromResult<decimal>(order.Amount * Convert.ToDecimal(discount) * 0.9m);
    //            case 20:
    //                return Task.FromResult<decimal>(order.Amount * Convert.ToDecimal(discount) * 0.7m);
    //            default:
    //                return Task.FromResult<decimal>(order.Amount * Convert.ToDecimal(discount));
    //        }
    //    }


    //    protected override Task OnActivateAsync()
    //    {
    //        Console.WriteLine($"Activating actor id:{this.Id}");
    //        return Task.CompletedTask;
    //    }

    //    protected override Task OnDeactivateAsync()
    //    {
    //        Console.WriteLine($"Deactivating actor id:{this.Id}");
    //        return Task.CompletedTask;
    //    }

    //    public async Task<string> SetOrderAsync(Order order)
    //    {
    //        await this.StateManager.SetStateAsync<Order>("order", order);
    //        return "Success";
    //    }
    //    public Task<Order> GetOrderAsync()
    //    {
    //        return this.StateManager.GetStateAsync<Order>("order");
    //    }

    //    public async Task RegisterReminder()
    //    {
    //        await this.RegisterReminderAsync("MyReminder",
    //        null,
    //        TimeSpan.FromSeconds(5),
    //        TimeSpan.FromSeconds(5));
    //    }

    //    public Task UnregisterReminder()
    //    {
    //        Console.WriteLine("Unregistering MyReminder");
    //        return this.UnregisterReminderAsync("MyReminder");
    //    }

    //    public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    //    {
    //        Console.WriteLine("ReceiveReminderAsync is called!");
    //        return Task.CompletedTask;
    //    }

    //    public Task RegisterTimer()
    //    {
    //        return this.RegisterTimerAsync(
    //            "MyTimer",// The name of the timer
    //            nameof(this.OnTimerCallBack),// Timer callback
    //            null,// User state passed to OnTimerCallback()
    //            TimeSpan.FromSeconds(5),// Time to delay before the async callback is first invoked
    //            TimeSpan.FromSeconds(5));// Time interval between invocations of the async callback
    //    }
    //    private Task OnTimerCallBack(byte[] data)
    //    {
    //        Console.WriteLine("OnTimerCallBack is called!");
    //        return Task.CompletedTask;
    //    }

    //    public Task UnregisterTimer()
    //    {
    //        Console.WriteLine("Unregistering MyTimer...");
    //        return this.UnregisterTimerAsync("MyTimer");
    //    }
    //}

}
