using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class PasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            char[] password = value.ToString().ToCharArray();

            if (password.Length < 8)
            {
                return new ValidationResult("password length must be at least 8 characters");
            }

            bool hasUpper = password.Any(p => p > 64 && p < 91);    // A - Z
            if (!hasUpper)
            {
                return new ValidationResult("password must contain at least one upper character");
            }

            bool hasLower = password.Any(p => p > 96 && p < 123);   // a - z
            if (!hasLower)
            {
                return new ValidationResult("password must contain at least one lower character");
            }

            bool hasSpaecial = password.Any(p => p > 32 && p < 48);   // !, @,.....
            if (!hasSpaecial)
            {
                return new ValidationResult("password must contain at least one special character");
            }

            bool hasNumeric = password.Any(p => p > 47 && p < 58);
            if (!hasNumeric)
            {
                return new ValidationResult("password must contain at least one numeric character");
            }

            return ValidationResult.Success;
        }
    }
}
