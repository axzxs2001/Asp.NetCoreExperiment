using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidation();
builder.Services.AddScoped<IValidator<Person>, PersonValidator>();
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();


app.MapPost("/person", async (IValidator<Person> validator, Person person) =>
 {
     var result = await validator.ValidateAsync(person);
     if (!result.IsValid)
     {
         var errors = new StringBuilder();
         foreach (var valid in result.Errors)
         {
             errors.AppendLine(valid.ErrorMessage);
         }
         return errors.ToString();
     }
     return "OK";
 });


app.MapPost("/person1", async (IValidatorFactory validatorFactory, Person person) =>
{
    var result = await validatorFactory.GetValidator<Person>().ValidateAsync(person);
    if (!result.IsValid)
    {
        var errors = new StringBuilder();
        foreach (var valid in result.Errors)
        {
            errors.AppendLine(valid.ErrorMessage);
        }
        return errors.ToString();
    }
    return "OK";
});

app.MapPost("/person2", async (IValidatorFactory validatorFactory, Person person) =>
{
    var result = await validatorFactory.GetValidator(typeof(Person)).ValidateAsync(new ValidationContext<Person>(person));
    if (!result.IsValid)
    {
        var errors = new StringBuilder();
        foreach (var valid in result.Errors)
        {
            errors.AppendLine(valid.ErrorMessage);
        }
        return errors.ToString();
    }
    return "OK";
});


app.Run();

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tel { get; set; }
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
    public string IDCard { get; set; }
    public PersonAddress Address { get; set; }
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


/// <summary>
/// Person验证
/// </summary>
public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator(IPersonService personService)
    {
        RuleFor(p => p.Name).NotNull().NotEmpty();
        RuleFor(p => p.Email).NotNull().EmailAddress();
        RuleFor(p => p.Birthday).NotNull();
        RuleFor(p => p.IDCard)
            .NotNull()
            .NotEmpty()
            .Length(18)
            .When(p => (DateTime.Now > p.Birthday.AddYears(1)))
            .WithMessage(p => $"出生日期为{p.Birthday}，现在时间为{DateTime.Now},大于一岁，CardID值必填！");
        RuleFor(p => p.Tel).NotNull().Matches(@"^(\d{3,4}-)?\d{6,8}$|^[1]+[3,4,5,8]+\d{9}$").WithMessage("电话格式为：0000-0000000或13000000000");
        RuleFor(p => p.Address).NotNull();
        RuleFor(p => p.Address).SetValidator(new PersonAddressValidator());
        RuleFor(p => p.Id).Must(id => personService.IsExist(id)).WithMessage(p => $"不存在id={p.Id}的用户");
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

public interface IPersonService
{
    public bool IsExist(int id);
}
public class PersonService : IPersonService
{
    public bool IsExist(int id)
    {
        if (DateTime.Now.Second % 2 == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}