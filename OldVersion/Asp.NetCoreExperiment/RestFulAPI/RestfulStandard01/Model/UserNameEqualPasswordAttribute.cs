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
    /// 用户中用户名和密码的自定义验证规则
    /// </summary>
    public class UserNameEqualPasswordAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// 构造
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
        }     
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //这里写验证规则
            var user = (User)validationContext.ObjectInstance;
            if (user.UserName.Length == user.Password.Length)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
