using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationWebDemo01
{
    /// <summary>
    /// Person验证
    /// </summary>
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty();
            RuleFor(p => p.Email).NotNull().EmailAddress();
            RuleFor(p => p.Birthday).NotNull();
            RuleFor(p => p.IDCard)
                .NotNull()
                .When(p => (DateTime.Now > p.Birthday.AddYears(1)))
                .WithMessage(p => $"出生日期为{p.Birthday}，现在时间为{DateTime.Now},大于一岁，CardID值必填！")
                .NotEmpty()
                .When(p => (DateTime.Now > p.Birthday.AddYears(1)))
                .WithMessage(p => $"出生日期为{p.Birthday}，现在时间为{DateTime.Now},大于一岁，CardID值必填！")
                .Length(18)
                .When(p => (DateTime.Now > p.Birthday.AddYears(1)));
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
            RuleFor(a => a.Country).NotNull().NotEmpty();
            RuleFor(a => a.Province).NotNull().NotEmpty();
            RuleFor(a => a.City).NotNull().NotEmpty();
            RuleFor(a => a.County).NotNull().NotEmpty();
            RuleFor(a => a.Address).NotNull().NotEmpty();
            RuleFor(a => a.Postcode).NotNull().NotEmpty().Length(6);
        }
    }
}
