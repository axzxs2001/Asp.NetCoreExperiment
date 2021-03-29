using System;
using System.Dynamic;
using FluentValidation;
using FluentValidation.Results;

namespace FluentValidationDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person()
            {
                //少一位
                Tel = "1345346711",
                Name = "桂素伟",
                //格式错误
                Email = "axzxs2001#163.com",
                //设置生日，没有身份证
                Birthday = DateTime.Parse("2020-03-28 00:00:00"),

                Address = new PersonAddress()
                {
                    //邮编位数不对
                    Postcode = "12345"
                },
            };
            var validator = new PersonValidator();
            var results = validator.Validate(person);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("属性 " + failure.PropertyName + " 验证失败：" + failure.ErrorMessage);
                }
            }
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine(results.ToString("\r\n"));
        }
    }
    /// <summary>
    /// Person验证
    /// </summary>
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name).NotNull();
            RuleFor(p => p.Email).NotNull().EmailAddress();
            RuleFor(p => p.Birthday).NotNull();
            RuleFor(p => p.IDCard)
                .NotNull()
                .When(p => (DateTime.Now > p.Birthday.AddYears(1)))
                .WithMessage(p => $"出生日期为{p.Birthday}，现在时间为{DateTime.Now},大于一岁，CardID值必填！");
            RuleFor(p => p.Tel).NotNull().Matches(@"^(\d{3,4}-)?\d{6,8}$|^[1]+[3,4,5,8]+\d{9}$");
            RuleFor(p => p.Address).NotNull();
            RuleFor(p => p.Address).SetValidator(new PersonAddressValidator());
        }
    }
    /// <summary>
    /// Person Address验证
    /// </summary>
    public class PersonAddressValidator : AbstractValidator<PersonAddress>
    {
        public PersonAddressValidator()
        {
            RuleFor(a => a.Country).NotNull();
            RuleFor(a => a.Province).NotNull();
            RuleFor(a => a.City).NotNull();
            RuleFor(a => a.County).NotNull();
            RuleFor(a => a.Address).NotNull();
            RuleFor(a => a.Postcode).NotNull().Length(6);
        }
    }


    public class Person
    {
        public int Id { get; set; }
        public DateTime Birthday { get; set; }
        public string IDCard { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public PersonAddress Address { get; set; }
        public string Tel { get; set; }
    }
    public class PersonAddress
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
    }
}
