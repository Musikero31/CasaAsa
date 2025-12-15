using System.ComponentModel.DataAnnotations.Schema;

namespace CasaAsa.Data.Models
{
    public class Address : IEntityDefault
    {
        public int Id { get; set; }
        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Postcode { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public bool IsDefaultAddress { get; set; } = false;

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
