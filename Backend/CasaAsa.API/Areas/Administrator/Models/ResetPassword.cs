using System.ComponentModel.DataAnnotations;

namespace CasaAsa.API.Areas.Administrator.Models
{
    public class ResetPassword
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ResetPasswordToken { get; set; }
    }
}
