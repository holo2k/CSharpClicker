using CSharpClicker.Web.Infrastructure.Abstractions;
using System.Security.Claims;

namespace CSharpClicker.Web.Infrastructure.Implementations
{
    public class CurrentUserAccsessor : ICurrentUserAccessor
    {
        private IHttpContextAccessor contextAccessor;
        public CurrentUserAccsessor(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = new HttpContextAccessor();
        }
        public Guid GetCurrentUserId()
        {
            if(contextAccessor.HttpContext == null)
            {
                throw new InvalidOperationException("Cannot get HTTP context.");
            }

            var idValue = contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!Guid.TryParse(idValue, out var userId))
            {
                throw new InvalidOperationException("Cannot parse user ID.");
            }
            return userId;
        }
    }
}
