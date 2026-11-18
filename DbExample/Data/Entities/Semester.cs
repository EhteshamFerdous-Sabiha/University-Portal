using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities
{
    public class Semester
    {
        [Key]
        public int SemesterId { get; set; }

        public int AcademicYearId { get; set; }

        public string semesterName { get; set; }


        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }


    }
}
