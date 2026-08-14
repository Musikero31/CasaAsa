namespace CasaAsa.API.Areas.Administrator.Data
{
    public class AuthenticationResponse
    {
        public Guid? UserId { get; set; }
        public bool Success { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public List<string>? Roles { get; set; }
        public string? ErrorCode { get; set; }
        public List<string>? Errors { get; set; }
        public string Message { get;  set; }
    }
}
