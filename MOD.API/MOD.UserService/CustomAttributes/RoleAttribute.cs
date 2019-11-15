using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.UserService.CustomAttributes
{
    public class RoleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && (value.ToString().ToUpper().Equals("M")
                || value.ToString().ToUpper().Equals("U")
                || value.ToString().ToUpper().Equals("A")))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(
                "Incorrect role provided. Valid roles are M,U and A");
        }
    }
}
