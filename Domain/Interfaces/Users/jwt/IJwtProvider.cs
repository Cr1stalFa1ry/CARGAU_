using Domain.Models.User;

namespace Domain.Interfaces.Users.Jwt;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}
