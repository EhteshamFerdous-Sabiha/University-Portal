using System.ComponentModel.DataAnnotations;

namespace DbExample.Models
{
    public class LoginModel
    {
        [Required]

        public string Username { get; set; } = null!;
        
        public string Password { get; set; } = null!;

    }
}
