using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities
{
    public class RegistrationCourse
    {

        [Key]
        public int RegistrationCourseId { get; set; }

        public int RegistrationId { get; set; }

        public int OfferingId { get; set; }
        public decimal CreditCompleted { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }


    }
}
