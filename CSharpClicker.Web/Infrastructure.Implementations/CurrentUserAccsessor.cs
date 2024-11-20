using CSharpClicker.Web.Infrastructure.Abstractions;
using System.Security.Claims;

namespace CSharpClicker.Web.Infrastructure.Implementations
{
    public class CurrentUserAccsessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserAccsessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = new HttpContextAccessor();
        }
        public Guid GetCurrentUserId()
        {
            if(_contextAccessor.HttpContext == null)
            {
                throw new InvalidOperationException("Cannot get HTTP context.");
            }

            var idValue = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!Guid.TryParse(idValue, out var userId))
            {
                throw new InvalidOperationException("Cannot parse user ID.");
            }
            return userId;
        }
    }
}
