using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Interfaces.Users.Jwt;
using Domain.Models.User;

namespace CARGAU.Jwt;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    // используем nuget пакет BCrypt.Net-Next для создания токенов
    public string GenerateToken(User user)
    {
        // содержимое токена, его полезная нагрузка
        Claim[] claims = [
            new("userId", user.Id.ToString()),
            new("userName", user.UserName),
            new("email", user.Email),
            new("role", user.Role.ToString())
        ];

        // алгоритм кодировки токена
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        // создание токена
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiteMinutes)
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}