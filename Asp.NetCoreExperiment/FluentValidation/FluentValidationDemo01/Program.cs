using System;

using FluentValidation;
using FluentValidation.Results;

namespace FluentValidationDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer()
            {
                Surname = "John",
                Discount = 9,
            };
            CustomerValidator validator = new CustomerValidator();

            ValidationResult results = validator.Validate(customer);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("属性 " + failure.PropertyName + " 验证失败：" + failure.ErrorMessage);
                }
            }
            Console.WriteLine("----------------");
            ValidationResult results1 = validator.Validate(customer);
            string allMessages = results1.ToString("\r\n");
            Console.WriteLine(allMessages);
        }
    }
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Name).NotNull();
            RuleFor(customer => customer.Discount).GreaterThan(10);
            RuleFor(c => c.Email).NotNull().EmailAddress();
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public decimal Discount { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
   
    }
}
