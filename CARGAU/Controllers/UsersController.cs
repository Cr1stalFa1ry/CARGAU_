using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Users.Commands.CreateNewUser;

namespace CARGAU.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("/register")]
    public async Task<IResult> Register([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
    {
        var (rt, jwt) = await _mediator.Send(request, cancellationToken);

        Response.Cookies.Append("auth-cookies", jwt, new CookieOptions()
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(1)
        });

        return Results.Ok(new {
            RefreshToken = rt,
            Jwt = jwt
        });
    }
}
