using System.ComponentModel.DataAnnotations;

namespace DbExample.CustomAttribute
{
    public class ShortCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
        
            if (value == null || value.ToString()!.Length != 3)
            {
                return new ValidationResult("Short code must be exactly 3 characters long.");
            }

         
            var nameProperty = validationContext.ObjectType.GetProperty("Name");
            if (nameProperty == null)
            {
                return new ValidationResult("Name property not found on the model.");
            }

           
            var nameValue = nameProperty.GetValue(validationContext.ObjectInstance);
            if (nameValue == null)
            {
                return new ValidationResult("Name cannot be null.");
            }

            
            if (value.ToString()!.Length > nameValue.ToString()!.Length)
            {
                return new ValidationResult("Short code must be shorter than the name.");
            }

            
            return ValidationResult.Success;
        }
    }
}