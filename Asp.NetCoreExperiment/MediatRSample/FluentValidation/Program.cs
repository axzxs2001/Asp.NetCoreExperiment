using FluentValidation;
using System;

namespace FluentValidationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer() { AginPassword = "111" };
            var validator = new CustomerValidator();
            var results = validator.Validate(customer, ruleSet: "Names");
            foreach (var error in results.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
    }
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleSet("Names", () =>
            {
                RuleFor(x => x.Surname).NotNull().WithName(x => "ddd");
                RuleFor(x => x.Forename).NotNull().WithMessage("surname empty");
                RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("不能为空");
                RuleFor(x => x.AginPassword).NotEmpty().NotNull().Equal(x => x.Password).WithMessage("要和Password相等");
                RuleFor(x => x.Surname).Must(surname =>
                {
                    return surname.Length > 0;

                });
            });
            //RuleFor(customer => customer.Surname).NotNull();
        }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public decimal Discount { get; set; }
        public string Address { get; set; }

        public string Password { get; set; }
        public string AginPassword { get; set; }
    }
}
