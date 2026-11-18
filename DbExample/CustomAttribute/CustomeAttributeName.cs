using System.ComponentModel.DataAnnotations;

namespace DbExample.CustomAttribute
{
    public class CustomeAttributeName: ValidationAttribute

    {
        public override bool IsValid(object? value)
        {
            if(value != null && value.ToString() is string s && s.ToLower().StartsWith("department of"))
            {
                return true;
            }
            return false;
        }
    }
}
