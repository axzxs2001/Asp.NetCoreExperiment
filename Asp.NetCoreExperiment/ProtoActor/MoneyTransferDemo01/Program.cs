using Proto;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace MoneyTransferDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Account : IActor
    {
        decimal _balance;
        Random _random;
        Dictionary<PID, object> _processedMessages;
        public Account()
        {
            _random = new Random();
            _processedMessages = new Dictionary<PID, object>();
        }
        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                // ...
                case Credit msg:
                    return AdjustBalance(msg.ReplyTo, msg.Amount);
                case Debit msg when msg.Amount + _balance >= 0:
                    return AdjustBalance(msg.ReplyTo, msg.Amount);
                    // ...
            }
            return Actor.Done;
        }

        private Task AdjustBalance(PID replyTo, decimal amount)
        {
            if (RefusePermanently())
            {
                _processedMessages.Add(replyTo, new Refused());
                replyTo.Tell(new Refused());
            }

            if (Busy())
                replyTo.Tell(new ServiceUnavailable());

            var behaviour = DetermineProcessingBehavior();
            if (behaviour == Behavior.FailBeforeProcessing)
                return Failure(replyTo);

            // simulate potential slow service
            Thread.Sleep(_random.Next(0, 150));

            _balance += amount;
            _processedMessages.Add(replyTo, new OK());


            if (behaviour == Behavior.FailAfterProcessing)
                return Failure(replyTo);

            replyTo.Tell(new OK());
            return Actor.Done;
        }


        Task Failure(PID replyTo)
        {
            return Task.CompletedTask;
        }
        private bool Busy()
        {
            throw new NotImplementedException();
        }

        private dynamic DetermineProcessingBehavior()
        {
            throw new NotImplementedException();
        }

        private bool RefusePermanently()
        {
            throw new NotImplementedException();
        }
    }
    public class OK
    {

    }
    public class ServiceUnavailable
    {

    }
    public class Refused
    {

    }
    public class InternalServerError
    {

    }

    public class Credit
    {
        public Credit(decimal amount, PID sender)
        {
            ReplyTo = sender;
            Amount = amount;
        }
        public PID ReplyTo { get; set; }
        public decimal Amount { get; set; }

    }
    public class Debit
    {
        public Debit(decimal amount,PID sender)
        {
            ReplyTo = sender;
            Amount = amount;
        }
        public PID ReplyTo { get; set; }
        public decimal Amount { get; set; }
    }
    class AccountProxy : IActor
    {
        private readonly PID _account;
        private readonly Func<PID, object> _createMessage;

        public AccountProxy(PID account, Func<PID, object> createMessage)
        {
            _account = account;
            _createMessage = createMessage;
        }

        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Started _:
                    _account.Tell(_createMessage(context.Self));
                    context.SetReceiveTimeout(TimeSpan.FromMilliseconds(100));
                    break;
                case OK msg:
                    context.CancelReceiveTimeout();
                    context.Parent.Tell(msg);
                    break;
                case Refused msg:
                    context.CancelReceiveTimeout();
                    context.Parent.Tell(msg);
                    break;
                // These represent a failed remote call
                case InternalServerError _:
                case ReceiveTimeout _:
                case ServiceUnavailable _:
                    throw new Exception();
            }

            return Actor.Done;
        }
    }

    public class TransferProcess : IActor
    {
        private readonly Behavior _behavior;

        private PID _from, _to;
        decimal _amount;

        public TransferProcess()
        {
            _behavior = new Behavior();
        }

        public Task ReceiveAsync(IContext context)
        {
            return _behavior.ReceiveAsync(context);
        }

        private async Task Starting(IContext context)
        {
            if (context.Message is Started)
            {
                context.SpawnNamed(TryDebit(_from, -_amount), "DebitAttempt");
                _behavior.Become(AwaitingDebitConfirmation);
            }
        }

        private Props TryDebit(PID targetActor, decimal amount) => Actor
                    .FromProducer(() => new AccountProxy(targetActor, sender => new Debit(amount, sender)));

        private Task AwaitingDebitConfirmation(IContext context)
        {
            switch (context.Message)
            {
                case OK _:
                    _behavior.Become(AwaitingCreditConfirmation);
                    context.SpawnNamed(TryCredit(_to, +_amount), "CreditAttempt");
                    break;
                case Refused _:
                    //_logger.Log("Transfer failed. System consistent")
                    StopAll(context);
                    break;
                case Terminated _:
                    // _logger.Log("Transfer status unknown. Escalate")
                    StopAll(context);
                    break;
            }
            return Task.CompletedTask;
        }

        private Props TryCredit(PID targetActor, decimal amount) => Actor
                    .FromProducer(() => new AccountProxy(targetActor, sender => new Credit(amount, sender)));

        private async Task AwaitingCreditConfirmation(IContext context)
        {
            switch (context.Message)
            {
                //...
                case OK msg:
                    // _logger.Log("Success!")
                    StopAll(context);
                    break;
                case Refused msg:
                    _behavior.Become(RollingBackDebit);
                    Actor.SpawnNamed(TryCredit(_from, +_amount), "RollbackDebit");
                    break;
                case Terminated msg:
                    // _logger.Log("Transfer status unknown. Escalate")
                    StopAll(context);
                    break;
            }
        }

        private async Task RollingBackDebit(IContext context)
        {
            switch (context.Message)
            {
                //...
                case OK _:
                    // _logger.Log("Transfer failed. System consistent")
                    StopAll(context);
                    break;
                case Refused _:
                case Terminated _:
                    //  _logger.Log("Transfer status unknown. Escalate")
                    StopAll(context);
                    break;
            }
        }

        private void StopAll(IContext context)
        {
            throw new NotImplementedException();
        }
    }

}
