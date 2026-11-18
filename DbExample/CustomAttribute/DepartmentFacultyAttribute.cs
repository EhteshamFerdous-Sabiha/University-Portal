using System.ComponentModel.DataAnnotations;

namespace DbExample.CustomAttribute
{
    public class DepartmentFacultyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var nameProperty = validationContext.ObjectType
                .GetProperty("Name");

            var name = nameProperty?
                .GetValue(validationContext.ObjectInstance)?
                .ToString();

            if (name?.Trim().Equals(
                    "Department of EEE",
                    StringComparison.OrdinalIgnoreCase) == true)
            {
                if (value is int facultyId && facultyId != 2)
                {
                    return new ValidationResult(
                        "Department of EEE must belong to FE.");
                }



            }


            if (name?.Trim().Equals(
                   "Department of FST",
                   StringComparison.OrdinalIgnoreCase) == true)
            {
                if (value is int facultyId && facultyId != 0)
                {
                    return new ValidationResult(
                        "Department of FST must belong to FST.");
                }



            }



            if (name?.Trim().Equals(
                 "Department of FBA",
                 StringComparison.OrdinalIgnoreCase) == true)
            {
                if (value is int facultyId && facultyId != 4)
                {
                    return new ValidationResult(
                        "Department of FBA must belong to FBA.");
                }



            }

            if (name?.Trim().Equals(
                   "Department of FASS",
                   StringComparison.OrdinalIgnoreCase) == true)
            {
                if (value is int facultyId && facultyId != 5)
                {
                    return new ValidationResult(
                        "Department of FASS must belong to FASS.");
                }



            }

           



            return ValidationResult.Success;
        }
    }
}   