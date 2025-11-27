using CasaAsa.Core.BusinessModels.Authentication;

namespace CasaAsa.Business.Component.Authentication
{
    public class AuthenticationResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public AuthenticationToken TokenResponse { get; set; }
    }
}
