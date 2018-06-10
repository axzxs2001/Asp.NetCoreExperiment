using FluentValidation;
using System;
using System.Collections.Generic;

namespace FluentValidationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer() {
                Address =new Address ()
            };
            var validator = new CustomerValidator();
            var results = validator.Validate(customer);
            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
        }
    }
    /// <summary>
    /// 验证体
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Name).NotEmpty().WithMessage(x => "姓名不能为空！");
            RuleFor(customer => customer.ID).GreaterThan(0).WithName(x => "编号");
            RuleFor(customer => customer.Password).NotEmpty().WithMessage("密码不能为空");
            RuleFor(customer => customer.SurePassword).Equal(customer => customer.Password).WithMessage("确认密码要与密码相同");
            RuleFor(customer => customer.Age).Custom((age, context) =>
            {
                if(age<18||age>45)
                {
                    context.AddFailure("年龄必需在18和45之间");
                }
            });

            RuleForEach(customer => customer.Titles).NotEmpty().WithMessage("每个Title不能为空");

            RuleFor(customer => customer.Address).SetValidator(new AddressValidator());

        }
    }
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Postcode).NotEmpty().WithMessage("地址邮编不能为空");
       
        }
    }
    /// <summary>
    /// 实体类
    /// </summary>
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public string SurePassword { get; set; }

        public int Age { get; set; }

        public List<string> Titles { get; set; } = new List<string>() {"厂长",""};

        public Address Address { get; set; }

    }

    public class Address
    {
        public string Postcode
        { get; set; }
        public string County
        { get; set; }

        public string Town
        { get; set; }
    }
}
