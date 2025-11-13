using Domain.Interfaces.Users.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.ServicesForApi;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserEmail()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        return user!.FindFirst(ClaimTypes.Email)!.Value;
    }

    public Guid? GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null) 
            throw new ArgumentNullException("Пользователь не существует.");

        var userIdClaim = user.FindFirst("userId")?.Value;
        return userIdClaim != null ? Guid.Parse(userIdClaim) : null;
    }
}
