using System.Security.Cryptography;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Users.IRefreshTokenProvider;
using Domain.Interfaces.Users.Services;
using Domain.Models.User;

namespace Application.ServicesForApi;

public class RefreshTokenProvider : IRefreshTokenProvider
{
    private readonly IRefreshTokenRepository _rtRep;
    private readonly IUserContextService _userContextService;
    public RefreshTokenProvider(
        IRefreshTokenRepository rtRep, 
        IUserContextService userContextService)
    {
        _rtRep = rtRep;
        _userContextService = userContextService;
    }
    public RefreshToken GenerateRefreshToken(User user)
    {
         return new RefreshToken(
            Guid.NewGuid(),
            GenerateToken(),
            user.Id,
            DateTime.UtcNow.AddDays(7)
        );
    }

    private string GenerateToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    

    public async Task<bool> RevokeRefreshToken(Guid userId)
    {
         // другой пользователь не может удалить чужой токен
        if (userId != _userContextService.GetCurrentUserId())
        {
            throw new ArgumentException("Oops, you cant do this");
        }

        var res = await _rtRep.DeleteToken(userId);

        return res;
    }
}
