using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities
{
    public class Registration
    {

        [Key]

        public int RegistrationId { get; set; }

        public int studentId { get; set; }

        public string Status { get; set; } = string.Empty;

        public int ProgramId { get; set; }

        public int SemesterId { get; set; }



    }
}
