namespace CasaAsa.Core.BusinessModels.Authentication
{
    public class AuthenticationResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public string FullName { get; set; }
        public AuthenticationToken TokenResponse { get; set; }
    }
}
