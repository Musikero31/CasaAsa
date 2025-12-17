using CasaAsa.Core.Abstraction;
using System.Security.Claims;

namespace CasaAsa.API
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid UserId 
            => Guid.Parse(_contextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
