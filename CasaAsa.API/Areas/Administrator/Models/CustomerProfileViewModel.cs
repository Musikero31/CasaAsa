namespace CasaAsa.API.Areas.Administrator.Models
{
    public class CustomerProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public required string ContactPerson { get; set; }
        public required string ContactNumber { get; set; }

        public List<AddressViewModel> Addresses { get; set; }
    }
}