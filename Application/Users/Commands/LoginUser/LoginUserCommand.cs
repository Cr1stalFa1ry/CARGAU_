using MediatR;

namespace Application.Users.Commands.LoginUser;

public record class LoginUserCommand(string Email, string Password) : IRequest<(string RefreshToken, string AccessToken)>;
