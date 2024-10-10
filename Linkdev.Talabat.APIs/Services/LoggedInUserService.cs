using System.Security.Claims;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;

namespace Linkdev.Talabat.APIs.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public string? UserId { get; }

        public LoggedInUserService(IHttpContextAccessor? httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            UserId = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
