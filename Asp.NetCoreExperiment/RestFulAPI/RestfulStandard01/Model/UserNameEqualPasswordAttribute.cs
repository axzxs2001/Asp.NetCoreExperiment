using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RestfulStandard01.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01
{
    public class UserNameEqualPasswordAttribute : ValidationAttribute, IClientModelValidator
    {
        public UserNameEqualPasswordAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            throw new NotImplementedException();
        }
        public string ErrorMessage { get; private set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;

            if (user.UserName.Length == user.Password.Length)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
