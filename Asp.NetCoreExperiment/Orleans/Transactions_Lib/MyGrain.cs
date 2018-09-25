using Orleans.CodeGeneration;
using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;
using System;
using System.Threading.Tasks;
using Transactions_Lib.Grain;

[assembly: GenerateSerializer(typeof(Balance))]
namespace Transactions_Lib.Grain
{
    [Serializable]
    public class Balance
    {
        public uint Value { get; set; } = 1000;
    }

    public class AccountGrain : Orleans.Grain, IAccountGrain
    {
        private readonly ITransactionalState<Balance> balance;

        public AccountGrain(
            [TransactionalState("balance")] ITransactionalState<Balance> balance)
        {
            this.balance = balance ?? throw new ArgumentNullException(nameof(balance));
        }

        Task IAccountGrain.Deposit(uint amount)
        {
            //this.balance.State.Value += amount;
            var random = new Random();
            if (random.Next(1, 10) % 2 == 0)
            {
                throw new Exception("11111");
            }
            else
            {
                balance.PerformUpdate(b => b.Value += amount);
                //this.balance.Save();
                return Task.CompletedTask;
            }
        }

        Task IAccountGrain.Withdraw(uint amount)
        {
            //this.balance.State.Value -= amount;
            //this.balance.Save();
            balance.PerformUpdate(b => b.Value -= amount);
            return Task.CompletedTask;
        }

        Task<uint> IAccountGrain.GetBalance()
        {
            return balance.PerformRead(b => b.Value);
            //return Task.FromResult(this.balance.State.Value);
        }
    }
    [StatelessWorker]
    public class ATMGrain : Orleans.Grain, IATMGrain
    {
        public Task Transfer(Guid fromAccount, Guid toAccount, uint amountToTransfer)
        {
            return Task.WhenAll(
                this.GrainFactory.GetGrain<IAccountGrain>(fromAccount).Withdraw(amountToTransfer),
                this.GrainFactory.GetGrain<IAccountGrain>(toAccount).Deposit(amountToTransfer));
        }
    }
}