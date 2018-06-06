using FluentValidation;
using System;

namespace FluentValidationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer();
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
                RuleFor(x => x.Surname).NotNull().WithName(x=>"ddd");
                RuleFor(x => x.Forename).NotNull().WithMessage("surname empty");
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
    }
}
