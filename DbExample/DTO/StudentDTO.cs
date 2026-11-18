using System.ComponentModel.DataAnnotations;

namespace DbExample.DTO
{
    public class StudentDTO
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student ID/No is required.")]
        [Display(Name = "Student ID")]
        [StringLength(50)]
        public string StudentNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Student Name is required.")]
        [Display(Name = "Student Name")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Name must be at least 2 characters.")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Varsity Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Varsity Email")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        [StringLength(50)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Display(Name = "Validity (Expiration Date)")]
        [DataType(DataType.Date)]
        public DateOnly? Validity { get; set; }

        [Required(ErrorMessage = "Please select an academic program.")]
        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Active";
    }
}
