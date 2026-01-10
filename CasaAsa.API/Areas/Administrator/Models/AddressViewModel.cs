namespace CasaAsa.API.Areas.Administrator.Models
{
    public class AddressViewModel
    {
        public int AddressID { get; set; }
        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Postcode { get; set; }

        public required string ContactPerson { get; set; }
        public required string ContactNumber { get; set; }
        public bool IsDefaultAddress { get; set; } = false;
        public bool ContactIsSameAsUser { get; set; }
    }
}
