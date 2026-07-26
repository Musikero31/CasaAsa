using System.ComponentModel.DataAnnotations;

namespace CasaAsa.API.Areas.Administrator.Models
{
    public class ConfirmUserViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
