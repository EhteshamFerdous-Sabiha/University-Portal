using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExample.Data.Entities
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreditHours { get; set; }

        public int DepartmentId { get; set; }

        [StringLength(150)]
        public string? Prereq { get; set; } = "Nil";

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department? Department { get; set; }

        public virtual ICollection<CourseCatalog> CourseCatalogs { get; set; } = new List<CourseCatalog>();
    }
}
