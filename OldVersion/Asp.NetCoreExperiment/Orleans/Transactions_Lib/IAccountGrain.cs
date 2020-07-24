using Orleans;
using System;
using System.Threading.Tasks;
namespace Transactions_Lib
{
    public interface IAccountGrain : IGrainWithGuidKey
    {
        [Transaction(TransactionOptionAlias.Required)]
        //[Transaction(TransactionOption.Create)]
        Task Withdraw(uint amount);

        [Transaction(TransactionOptionAlias.Required)]
        //[Transaction(TransactionOption.Create)]
        Task Deposit(uint amount);

        [Transaction(TransactionOptionAlias.Required)]
        //[Transaction(TransactionOption.Create)]
        Task<uint> GetBalance();
    }
    public interface IATMGrain : IGrainWithIntegerKey
    {

        [Transaction(TransactionOptionAlias.RequiresNew)]
        // [Transaction(TransactionOption.Create)]
        Task Transfer(Guid fromAccount, Guid toAccount, uint amountToTransfer);
    }
}


