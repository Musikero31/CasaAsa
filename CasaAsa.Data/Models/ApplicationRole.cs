using Microsoft.AspNetCore.Identity;

namespace CasaAsa.Data.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<IdentityUserRole<string>>? UserRoles { get; set; }
    }
}
