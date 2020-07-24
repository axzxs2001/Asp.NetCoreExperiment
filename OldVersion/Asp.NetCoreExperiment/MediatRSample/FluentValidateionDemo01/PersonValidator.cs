using FluentValidation;
namespace FluentValidateionDemo01
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Surname).Custom((Surname, context) =>
            {
                var W = Surname;
                if (context.ParentContext.RootContextData.ContainsKey("MyCustomData"))
                {
                    context.AddFailure("My error message");
                }
            });

        }
    }
}