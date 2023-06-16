using System.ComponentModel.DataAnnotations;
using hospital_back.Enums;

namespace hospital_back.Models
{
    public class Login
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}
