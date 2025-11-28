using CasaAsa.Core.Common;

namespace CasaAsa.Core.BusinessModels.UserProfile
{
    public class Customer : AuditEntity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
