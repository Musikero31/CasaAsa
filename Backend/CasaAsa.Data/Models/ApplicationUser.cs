using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasaAsa.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}".Trim();

        public ICollection<Address>? Addresses { get; set; }

        public ICollection<IdentityUserRole<Guid>>? UserRoles { get; set; }
    }
}