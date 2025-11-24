using System.ComponentModel.DataAnnotations.Schema;

namespace CasaAsa.Data.Models
{
    public class Address : IEntityDefault
    {
        [NotMapped]
        public string FullAddress
        {
            get
            {
                string address1 = AddressLine1;
                string address2 = !string.IsNullOrEmpty(AddressLine2) ? $", {AddressLine2}" : string.Empty;
                string address3 = !string.IsNullOrEmpty(AddressLine3) ? $", {AddressLine3}" : string.Empty;

                return $"{address1}{address2}{address3}, {City}, {State} {Postcode}";
            }
        }
        public int Id { get; set; }
        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Postcode { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNumber { get; set; }

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
