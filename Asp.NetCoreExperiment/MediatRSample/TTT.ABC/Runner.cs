using MediatR;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTT.ABC
{

    public static class Runner
    {
        public static async Task Run(IMediator mediator, WrappingWriter writer)
        {
            //请求应答
            await writer.WriteLineAsync("请求：Request");
            var response = await mediator.Send(new MyRequest { Message = "Request" });
            await writer.WriteLineAsync("应答: " + response.Message);

            Console.WriteLine("=================================================");
            //订阅
            await mediator.Publish(new MyDomainEvent { ID = 1, Name = "你好" });

        }
    }



    #region Notification

    public class NotificationHandler1 : INotificationHandler<MyDomainEvent>
    {
        private readonly TextWriter _writer;

        public NotificationHandler1(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Handle(MyDomainEvent notification, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync($"NotificationHandler1处理：{notification}");
        }
    }

    public class NotificationHandler2 : INotificationHandler<MyDomainEvent>
    {
        private readonly TextWriter _writer;

        public NotificationHandler2(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Handle(MyDomainEvent notification, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync($"NotificationHandler2处理：{notification}");
        }
    }
    public class MyDomainEvent : INotification
    {
        public int ID
        { get; set; }

        public string Name
        { get; set; }

        public override string ToString()
        {
            return $"ID={ID},Name={Name}";
        }

    }
    #endregion

    #region 生命周期
    //public class GenericHandler : INotificationHandler<INotification>
    //{
    //    private readonly TextWriter _writer;

    //    public GenericHandler(TextWriter writer)
    //    {
    //        _writer = writer;
    //    }

    //    public Task Handle(INotification notification, CancellationToken cancellationToken)
    //    {
    //        return _writer.WriteLineAsync("Got notified.");
    //    }
    //}



    //public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    //{
    //    private readonly TextWriter _writer;

    //    public GenericRequestPostProcessor(TextWriter writer)
    //    {
    //        _writer = writer;
    //    }

    //    public Task Process(TRequest request, TResponse response)
    //    {
    //        return _writer.WriteLineAsync("- All Done");
    //    }
    //}
    //public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    //{
    //    private readonly TextWriter _writer;

    //    public GenericRequestPreProcessor(TextWriter writer)
    //    {
    //        _writer = writer;
    //    }

    //    public Task Process(TRequest request, CancellationToken cancellationToken)
    //    {
    //        return _writer.WriteLineAsync("- Starting Up");
    //    }
    //}
    #endregion


    public class WrappingWriter : TextWriter
    {
        private readonly TextWriter _innerWriter;
        private readonly StringBuilder _stringWriter = new StringBuilder();

        public WrappingWriter(TextWriter innerWriter)
        {
            _innerWriter = innerWriter;
        }

        public override void Write(char value)
        {
            _stringWriter.Append(value);
            _innerWriter.Write(value);
        }

        public override Task WriteLineAsync(string value)
        {
            _stringWriter.AppendLine(value);
            return _innerWriter.WriteLineAsync(value);
        }

        public override Encoding Encoding => _innerWriter.Encoding;

        public string Contents => _stringWriter.ToString();
    }
}
