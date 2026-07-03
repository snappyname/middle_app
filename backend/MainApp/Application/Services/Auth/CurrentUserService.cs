using Application.Services.Abstract.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public Guid UserId
    {
        get
        {
            var userId = User?.FindFirstValue(CustomClaims.UserId);
            return Guid.TryParse(userId, out var id) ? id : throw new Exception("Can't define user");
        }
    }

    public string Email => User?.FindFirstValue(CustomClaims.Email) ?? string.Empty;
    public bool IsAdmin => bool.TryParse(User?.FindFirstValue(CustomClaims.IsAdmin), out var isAdmin) && isAdmin;
}
