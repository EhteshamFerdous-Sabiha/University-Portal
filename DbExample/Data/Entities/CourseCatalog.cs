using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExample.Data.Entities
{
    [Table("CourseCatalogs")]
    public class CourseCatalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseCatalogId { get; set; }

        public int SemesterNo { get; set; }

        public int ProgramId { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(ProgramId))]
        public virtual Programs? Program { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual Course? Course { get; set; }
    }
}
