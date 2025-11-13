using System;

namespace Domain.Models.User;

public class RefreshToken
{
    public RefreshToken(Guid id, string token, Guid userId, DateTime expiresOnUtc)
    {
        Id = id;
        Token = token;
        UserId = userId;
        ExpiresOnUtc = expiresOnUtc;
    }
    public Guid Id { get; set; }
    public string Token { get; set; } = String.Empty;
    public Guid UserId { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
}
