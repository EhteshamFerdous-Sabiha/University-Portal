
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DbExample.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter department name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Enter short code")]
        public string ShortCode { get; set; } = null!;

        [Display(Name = "Faculty")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Please select a faculty")]
        public int FacultyId { get; set; }

        [ValidateNever]
        public string FacultyName { get; set; } = null!;
    }
}
