using System.ComponentModel.DataAnnotations;
using hospital_back.Enums;

namespace hospital_back.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public RoleType Role { get; set; }
        public decimal CRM { get; set; }
        public decimal CPF { get; set; }
        public decimal CellPhone { get; set; }
    }
}
