using CasaAsa.Business.Constants;
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

        public Guid? UserId
        {
            get
            {
                var userIdValue = _contextAccessor?
                    .HttpContext?
                    .User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(userIdValue, out var userId) 
                    ? userId 
                    : ApplicationConstants.SYSTEM_USER_ID;
            }
        }
    }
}
