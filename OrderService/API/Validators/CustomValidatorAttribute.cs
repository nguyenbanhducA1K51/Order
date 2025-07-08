namespace API.Validators;

using System.ComponentModel.DataAnnotations;

public class CustomValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Value cannot be null");
        }

        // Example: Validate that string is not "invalid"
        if (value is string stringValue && stringValue.ToLower() == "invalid")
        {
            return new ValidationResult("The value 'invalid' is not allowed");
        }

        return ValidationResult.Success;
    }
}