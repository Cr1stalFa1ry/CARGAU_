using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.LoginUser;

namespace CARGAU.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("/register")]
    public async Task<IResult> Register([FromBody] RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (refreshToken, jwt) = await _mediator.Send(request, cancellationToken);

        Response.Cookies.Append("auth-cookies", jwt, new CookieOptions()
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(1)
        });

        return Results.Created("users", new
        {
            RefreshToken = refreshToken,
            Jwt = jwt
        });
    }

    [HttpPost("/login")]
    public async Task<IResult> Login([FromBody] LoginUserCommand request, CancellationToken cancellationToken)
    {
        var (refreshToken, jwt) = await _mediator.Send(request, cancellationToken);

        Response.Cookies.Append("auth-cookies", jwt, new CookieOptions()
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(1)
        });

        return Results.Ok(new
        {
            RefreshToken = refreshToken,
            Jwt = jwt
        });
    }
}
