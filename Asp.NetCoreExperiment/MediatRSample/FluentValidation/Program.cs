using FluentValidation;
using System;
using System.Collections.Generic;

namespace FluentValidationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer()
            {
                SurePassword = "1",
                Address = new Address(),
                Sex = false,
                N1 = "a",
                Positions = new List<Position> {
                    new Position{ NO="no001"},
                    new Position{Name="name001"}
                }
            };
            var validator = new CustomerValidator();
            var results = validator.Validate(customer, ruleSet: "*");
            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine("Property name: " + error.PropertyName);
                    Console.WriteLine("Error: " + error.ErrorMessage);
                    Console.WriteLine("");
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
            RuleSet("sigle", () =>
            {
                RuleFor(customer => customer.Name).NotEmpty().WithMessage(x => "姓名不能为空！");
                RuleFor(customer => customer.ID).GreaterThan(0).WithName(x => "编号");
                RuleFor(customer => customer.Password).NotEmpty().WithMessage("密码不能为空");
                RuleFor(customer => customer.SurePassword).Equal(customer => customer.Password).WithMessage("确认密码要与密码相同");
                RuleFor(customer => customer.Age).Custom((age, context) =>
                {
                    if (age < 18 || age > 45)
                    {
                        context.AddFailure("年龄必需在18和45之间");
                    }
                });
                RuleFor(customer => customer.Address).SetValidator(new AddressValidator());
            });


            RuleSet("lists", () =>
            {
                RuleForEach(customer => customer.Titles).NotEmpty().WithMessage("每个Title不能为空");

            });

            //where是移除不作验证的
            RuleFor(customer => customer.Positions).SetCollectionValidator(new PositionValidator()).Where(x => x.NO != null);
            //条件验证
            When(customer => customer.Sex == true, () =>
            {
                RuleFor(customer => customer.Age).GreaterThan(20);
            });

            RuleFor(customer => customer.N1).NotNull().DependentRules(() =>
            {
                RuleFor(customer => customer.N2).NotNull();
            });
            // RuleFor(customer => customer.CascadeString).NotNull().WithMessage("CascadeString为Null了").NotEmpty().WithMessage("CascadeString 空了").NotEqual("foo").WithMessage("CascadeString等于foo了");
            RuleFor(customer => customer.CascadeString).Cascade(CascadeMode.StopOnFirstFailure).NotNull().WithMessage("CascadeString为Null了").NotEmpty().WithMessage("CascadeString 空了").NotEqual("foo").WithMessage("CascadeString等于foo了");
        }
    }
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Postcode).Length(7).WithMessage("邮编必需就7位");
            RuleFor(address => address.County).NotEmpty().WithMessage("国家不能为空");

        }
    }
    public class PositionValidator : AbstractValidator<Position>
    {
        public PositionValidator()
        {
            RuleFor(position => position.NO).NotEmpty().WithMessage("NO不能为空");
            RuleFor(position => position.Name).NotEmpty().WithMessage("Name不能为空");

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

        public List<string> Titles { get; set; } = new List<string>() { "厂长", "" };

        public Address Address { get; set; }

        public List<Position> Positions { get; set; }

        public bool Sex { get; set; }

        public string N1 { get; set; }
        public string N2 { get; set; }

        public string CascadeString { get; set; }


    }
    public class Position
    {
        public string NO { get; set; }
        public string Name { get; set; }
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
