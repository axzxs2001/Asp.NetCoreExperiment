using FluentValidation;
using System;

namespace FluentValidateionDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person(){ Surname="abc"};
            var context = new ValidationContext<Person>(person);
            context.RootContextData["MyCustomData"] = "Test";
            var validator = new PersonValidator();           

            var results = validator.Validate(context);
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
}
