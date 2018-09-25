using Orleans;
using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;
using System;
using System.Threading.Tasks;
namespace Transactions_Lib
{
    public interface IAccountGrain : IGrainWithGuidKey
    {
        [Transaction(TransactionOption.Create)]
        Task Withdraw(uint amount);

        [Transaction(TransactionOption.Create)]
        Task Deposit(uint amount);

        [Transaction(TransactionOption.Create)]
        Task<uint> GetBalance();
    }
    public interface IATMGrain : IGrainWithIntegerKey
    {
        [Transaction(TransactionOption.Create)]
        Task Transfer(Guid fromAccount, Guid toAccount, uint amountToTransfer);
    }
}


