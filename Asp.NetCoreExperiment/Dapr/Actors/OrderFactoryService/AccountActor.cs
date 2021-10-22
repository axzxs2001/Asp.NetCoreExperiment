using Dapr.Actors.Runtime;
using IOrderFactoryActory.Interfaces;

namespace OrderFactoryService
{
    public class AccountActor : Actor, IAccountActor
    {
        public AccountActor(ActorHost host) : base(host)
        {
        }

        public async Task<decimal> ChargeAsync(decimal amount)
        {
            var balance = 0m;
            var balanceValue = await this.StateManager.TryGetStateAsync<decimal>("balance");
            if (balanceValue.HasValue)
            {
                balance = balanceValue.Value;
            }
            balance += amount;
            await this.StateManager.SetStateAsync<decimal>("balance", balance);
            return balance;
        }

        public async Task<string> GetTimeAsync(string inTime)
        {
            Console.WriteLine($"{this.Id}开始");
            Task.Delay(3000).Wait();
            Console.WriteLine($"{this.Id}结束");
            return await Task.FromResult($"Actor ID:{this.Id} 传入时间：{inTime}，返回时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
        }
    }
}
