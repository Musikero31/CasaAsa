using System.ComponentModel.DataAnnotations;

namespace CasaAsa.API.Areas.Administrator.Models
{
    public class ResendConfirmationViewModel
    {
        [Required]
        public required string Username { get; set; }
    }
}
