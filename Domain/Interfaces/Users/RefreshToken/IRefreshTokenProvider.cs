using Domain.Models.User;

namespace Domain.Interfaces.Users.IRefreshTokenProvider;

public interface IRefreshTokenProvider
{
    RefreshToken GenerateRefreshToken(User user);
    Task<bool> RevokeRefreshToken(Guid userId);
}
