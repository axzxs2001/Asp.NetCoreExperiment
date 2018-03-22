using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRDemo
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Surname).NotEmpty();
            RuleFor(customer => customer.Forename).NotEmpty().WithMessage("Please specify a first name");
            RuleFor(customer => customer.Discount).NotEqual(0).When(customer => customer.HasDiscount);
            RuleFor(customer => customer.Address).Length(20, 250);
            RuleFor(customer => customer.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
    public static class Program
    {
        public static Task Main(string[] args)
        {
            TestFluentValidation();
            var writer = Console.Out;
            return Run(CreateMediator(writer), writer);
        }
        static void TestFluentValidation()
        {
            Customer customer = new Customer();
            CustomerValidator validator = new CustomerValidator();
            ValidationResult results = validator.Validate(customer);

            bool validationSucceeded = results.IsValid;
            IList<ValidationFailure> failures = results.Errors;
        }


        static IMediator CreateMediator(TextWriter writer)
        {

            IServiceCollection services = new ServiceCollection();
            services.AddScoped<SingleInstanceFactory>(p => p.GetRequiredService);
            services.AddScoped<MultiInstanceFactory>(p => p.GetRequiredServices);

            services.AddSingleton<TextWriter>(writer);

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator), typeof(Request))
                .AddClasses()
                .AsImplementedInterfaces());

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
            return mediator;
        }
        static async Task Run(IMediator mediator, TextWriter writer)
        {
            writer.WriteLine("请求：Request");
            var response = await mediator.Send(new Request { Message = "Request" });
            writer.WriteLine("应答: " + response.Message);
            writer.WriteLine("------------------------------");


            await mediator.Publish(new MyDomainEvent { ID = 1, Name = "你好" });
            Console.ReadLine();
        }

        private static IEnumerable<object> GetRequiredServices(this IServiceProvider provider, Type serviceType)
        {
            return (IEnumerable<object>)provider.GetRequiredService(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }
    }

    public class Customer
    {
        public string Surname { get; set; }

        public string Forename { get; set; }
        public bool HasDiscount { get; set; }
        public int? Discount { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
    }

    #region Request/Response

    public class Request : IRequest<Response>
    {
        public string Message
        { get; set; }
    }

    public class Response
    {
        public string Message { get; set; }

    }
    public class PingHandler : IRequestHandler<Request, Response>
    {
        private readonly TextWriter _writer;
        public PingHandler(TextWriter writer)
        {
            _writer = writer;
        }
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            await _writer.WriteLineAsync($"这里收到请求： {request.Message}");
            return new Response { Message = request.Message + " Response" };
        }
    }
    #endregion

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
}
