namespace CasaAsa.Core.BusinessModels.UserProfile
{
    public class Address
    {
        public int AddressID { get; set; }
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
        public int UserID { get; set; }
        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Postcode { get; set; }
        public required string ContactPerson { get; set; }
        public required string ContactNumber { get; set; }
    }
}
