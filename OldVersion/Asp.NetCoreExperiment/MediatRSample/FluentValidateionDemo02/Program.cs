using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;

namespace FluentValidateionDemo02
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            var personValidator = new PersonValidator();
            var results = personValidator.Validate(person);
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


    public class ListCountValidator<T> : PropertyValidator
    {
        private int _max;

        public ListCountValidator(int max)
            : base("{PropertyName} must contain fewer than {MaxElements} items.")
        {
            _max = max;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var list = context.PropertyValue as IList<T>;

            if (list != null && list.Count >= _max)
            {
                context.MessageFormatter.AppendArgument("MaxElements", _max);
                return false;
            }

            return true;
        }
    }


    /// <summary>
    /// 定义自定义验证
    /// </summary>
    public static class MyCustomValidators
    {
        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            // return ruleBuilder.Must(list => list.Count > num).WithMessage("The list contains too many items");
            /*  return ruleBuilder.Must((rootObject, list, context) => {
                  context.MessageFormatter.AppendArgument("MaxElements", num);
                  return list.Count > num;
              })
         .WithMessage("{PropertyName} must contain fewer than {MaxElements} items.");*/

            /* return ruleBuilder.Must((rootObject, list, context) =>
             {
                 context.MessageFormatter
                   .AppendArgument("MaxElements", num)
                   .AppendArgument("TotalElements", list.Count);

                 return list.Count < num;
             })
 .WithMessage("{PropertyName} must contain fewer than {MaxElements} items. The list contains {TotalElements} element");*/
            return ruleBuilder.SetValidator(new ListCountValidator<TElement>(num));
        }
    }


    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            //原生验证
            // RuleFor(x => x.Pets).Must(list => list.Count >10).WithMessage("The list must contain fewer than 10 items");
            //使用扩展类
            //RuleFor(x => x.Pets).ListMustContainFewerThan(0);
            //自定义失败
            /* RuleFor(x => x.Pets).Custom((list, context) => {
                 if (list.Count <= 0)
                 {
                     context.AddFailure("The list must contain 0 items or fewer");
                 }
             });*/

            //
            RuleFor(person => person.Pets).SetValidator(new ListCountValidator<Pet>(10));
        }
    }
    public class Person
    {
        public IList<Pet> Pets { get; set; } = new List<Pet>();
    }
    public class Pet
    {

    }
}
