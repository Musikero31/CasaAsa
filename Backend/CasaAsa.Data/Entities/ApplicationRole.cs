using Microsoft.AspNetCore.Identity;

namespace CasaAsa.Data.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<IdentityUserRole<Guid>>? UserRoles { get; set; }
    }
}
