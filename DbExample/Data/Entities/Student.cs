using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExample.Data.Entities
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [StringLength(50)]
        public string StudentNo { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        public string Phone { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public DateOnly? Validity { get; set; }

        [Required]
        public int ProgramId { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [ForeignKey(nameof(ProgramId))]
        public virtual Programs? Program { get; set; }
    }
}
