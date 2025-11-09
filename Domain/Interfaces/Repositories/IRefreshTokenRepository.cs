using Domain.Models.User;

namespace Domain.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task AddToken(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task<bool> DeleteToken(Guid userId, CancellationToken cancellationToken = default);
}
