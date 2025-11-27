namespace CasaAsa.Core.BusinessModels.Authentication
{
    public class AuthenticationToken
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
