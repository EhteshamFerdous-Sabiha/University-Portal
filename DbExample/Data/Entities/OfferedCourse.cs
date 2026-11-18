using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities
{
    public class OfferedCourse
    {
        [Key]
        public int OfferedCourseId { get; set; }

        public int Capacity { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public int FacultyMemberId { get; set; }
        
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
