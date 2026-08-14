using System.ComponentModel.DataAnnotations;

namespace CasaAsa.API.Areas.Administrator.Models
{
    public class LoginViewModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
