using System.ComponentModel.DataAnnotations;

namespace CasaAsa.API.Areas.Administrator.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
