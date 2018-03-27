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

namespace TTT.ABC.MediatRDemo
{
    //public class CustomerValidator : AbstractValidator<Customer>
    //{
    //    public CustomerValidator()
    //    {
    //        RuleFor(customer => customer.Surname).NotEmpty();
    //        RuleFor(customer => customer.Forename).NotEmpty().WithMessage("Please specify a first name");
    //        RuleFor(customer => customer.Discount).NotEqual(0).When(customer => customer.HasDiscount);
    //        RuleFor(customer => customer.Address).Length(20, 250);
    //        RuleFor(customer => customer.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
    //    }

    //    private bool BeAValidPostcode(string postcode)
    //    {
    //        // custom postcode validating logic goes here
    //        return true;
    //    }
    //}
    public static class Program
    {
        public static Task Main(string[] args)
        {
           // TestFluentValidation();
            var writer = new WrappingWriter(Console.Out);
            return Runner.Run(CreateMediator(writer), writer);
        }
        //static void TestFluentValidation()
        //{
        //    Customer customer = new Customer();
        //    CustomerValidator validator = new CustomerValidator();
        //    ValidationResult results = validator.Validate(customer);

        //    bool validationSucceeded = results.IsValid;
        //    IList<ValidationFailure> failures = results.Errors;
        //}


        static IMediator CreateMediator(WrappingWriter writer)
        {

            var services = new ServiceCollection();

            services.AddScoped<SingleInstanceFactory>(p => p.GetRequiredService);
            services.AddScoped<MultiInstanceFactory>(p => p.GetRequiredServices);

            services.AddSingleton<TextWriter>(writer);

            //Pipeline 注意，如果Scrutoro为2.1.2版本，需要添加Pipeline注入，最新版2.2.2不需要，引入会两次调用。
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));


            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));


            // services.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
            // services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>));

            services.Scan(scan => scan
                    .FromAssembliesOf(typeof(IMediator), typeof(MyRequest))
                    .AddClasses()
                    .AsImplementedInterfaces());

            var provider = services.BuildServiceProvider();

            return provider.GetRequiredService<IMediator>();
        }

        private static IEnumerable<object> GetRequiredServices(this IServiceProvider provider, Type serviceType)
        {
            return (IEnumerable<object>)provider.GetRequiredService(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }
    }

    //public class Customer
    //{
    //    public string Surname { get; set; }

    //    public string Forename { get; set; }
    //    public bool HasDiscount { get; set; }
    //    public int? Discount { get; set; }
    //    public string Address { get; set; }
    //    public string Postcode { get; set; }
    //}

}

