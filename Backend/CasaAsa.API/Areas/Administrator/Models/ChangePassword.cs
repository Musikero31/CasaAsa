using System.ComponentModel.DataAnnotations;

namespace CasaAsa.API.Areas.Administrator.Models
{
    public class ChangePassword
    {
        [Required]
        public required Guid UserId { get; set; }
        [Required]
        public required string NewPassword { get; set; }
        [Required]
        public required string Token { get; set; }
    }
}
