using System.Security.Claims;
using Application.Services.Abstract;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string UserId
    {
        get
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out var id) ? id.ToString() : throw new Exception("Can't define user");
        }
    }

    public string Email => User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
}
