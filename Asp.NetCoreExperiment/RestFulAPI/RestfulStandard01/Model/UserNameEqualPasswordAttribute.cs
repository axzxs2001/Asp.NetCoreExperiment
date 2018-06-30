using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RestfulStandard01.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01
{
    /// <summary>
    /// 
    /// </summary>
    public class UserNameEqualPasswordAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        public UserNameEqualPasswordAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void AddValidation(ClientModelValidationContext context)
        {
            throw new NotImplementedException();
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
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
