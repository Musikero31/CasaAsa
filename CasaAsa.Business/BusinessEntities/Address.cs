namespace CasaAsa.Business.BusinessEntities
{
    public class Address
    {
        public int AddressID { get; set; }
        public string FullAddress => $"{AddressLine1} {AddressLine2} {AddressLine3}".Trim();
        public int UserID { get; set; }
        public required string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Postcode { get; set; }
        public required string ContactPerson { get; set; }
        public required string ContactNumber { get; set; }
    }
}
