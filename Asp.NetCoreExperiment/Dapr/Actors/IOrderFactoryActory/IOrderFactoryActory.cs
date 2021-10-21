using Dapr.Actors;

namespace IOrderFactoryActory.Interfaces
{
    public interface IOrderFactoryActory : IActor
    {
        Task<string> GetTime(string inTime);
        Task<decimal> GetOrderAmountAsync(Order order);
        Task<string> SetOrderAsync(Order data);
        Task<Order> GetOrderAsync();
        Task RegisterReminder();
        Task UnregisterReminder();
        Task RegisterTimer();
        Task UnregisterTimer();
    }

    public record Order
    {
        public string OrderNo { get; set; }
        public decimal Amount { get; set; }
        public string OrderType { get; set; }
        public DateTime Expiration { get; set; }
        public int Quantity { get; set; }

    }
}