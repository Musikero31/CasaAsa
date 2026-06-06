namespace CasaAsa.API.Areas.Administrator.Data
{
    public class LoginResponse
    {
        public Guid? UserId { get; set; }
        public bool Succeeded { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public List<string>? Roles { get; set; }
        public List<string>? Errors { get; set; }
    }
}
