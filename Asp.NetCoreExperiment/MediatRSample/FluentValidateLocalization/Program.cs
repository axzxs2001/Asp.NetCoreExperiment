using FluentValidation;
using System;
using System.Globalization;

namespace FluentValidateLocalization
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidatorOptions.LanguageManager = new CustomLanguageManager();
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("en");
            var address = new Address();
            var validator = new AddressValidator();
            var results = validator.Validate(address);
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

    public class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("en", "NotNullValidator", "'{PropertyName}' is required.");
        }
    }
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Postcode).Length(7).WithMessage("邮编必需就7位");
            RuleFor(address => address.County).NotNull().NotEmpty();

        }
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
