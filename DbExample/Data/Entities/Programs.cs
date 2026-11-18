using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExample.Data.Entities
{
    [Table("Programs")]
    public class Programs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgramId { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; } = null!;

        [Column("TotalCredit", TypeName = "decimal(18, 2)")]
        [Display(Name = "Total Credits")]
        public decimal TotalCredit { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Duration (Years)")]
        public decimal DurationYears { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department? Department { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<CourseCatalog> CourseCatalogs { get; set; } = new List<CourseCatalog>();
    }
}
