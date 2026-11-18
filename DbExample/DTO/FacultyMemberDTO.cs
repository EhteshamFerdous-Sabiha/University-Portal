
using System.ComponentModel.DataAnnotations;

namespace DbExample.DTO
{
    public class FacultyMemberFormDTO
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "Name must contain 3 to 150 characters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Enter a valid phone number.")]
        [StringLength(50)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(100)]
        public string Designation { get; set; } = string.Empty;

        [StringLength(250)]
        [Display(Name = "Research Interest")]
        public string? ResearchInterest { get; set; }

        [Required(ErrorMessage = "Joining date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Joining Date")]
        public DateTime? JoinDate { get; set; }

        [Required(ErrorMessage = "Please select a department.")]
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
    }
}
