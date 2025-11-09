using Domain.Interfaces.Repositories;
using Domain.Interfaces.Users.IRefreshTokenProvider;
using Domain.Interfaces.Users.Jwt;
using Domain.Interfaces.Users.PasswordHasher;
using MediatR;

namespace Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler
    : IRequestHandler<LoginUserCommand, (string RefreshToken, string AccessToken)>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenProvider _rtProvider;
    private readonly IRefreshTokenRepository _rtRepository;
    private readonly IUserRepository _userRepository;
    public LoginUserCommandHandler(
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IRefreshTokenProvider rtProvider,
            IRefreshTokenRepository rtRepository,
            IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _rtProvider = rtProvider;
        _rtRepository = rtRepository;
        _userRepository = userRepository;
    }

    public async Task<(string RefreshToken, string AccessToken)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

            if (!_passwordHasher.Verify(request.Password, user.Password))
                throw new UnauthorizedAccessException("Неверный пароль.");

            var accessToken = _jwtProvider.GenerateToken(user);
            var refreshToken = _rtProvider.GenerateRefreshToken(user);

            await _rtRepository.AddToken(refreshToken, cancellationToken);

            return (refreshToken.Token, accessToken);
        }
        catch (InvalidOperationException ex)
        {
            // Пользователь не найден
            throw new ApplicationException("Ошибка входа: " + ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            // Неверный пароль
            throw new ApplicationException("Ошибка авторизации: " + ex.Message);
        }
        catch (OperationCanceledException)
        {
            // Операция отменена (например, запрос прерван)
            throw;
        }
        catch (Exception ex)
        {
            // Любая непредвиденная ошибка
            throw new ApplicationException("Произошла внутренняя ошибка при входе.", ex);
        }
    }

}
