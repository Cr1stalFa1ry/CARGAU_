using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.LoginUser;
using Application.Users.Queries.GetUsers;
using Application.Users.Queries.GetCurrentUser;

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

    [HttpGet("get-all-users")]
    public async Task<IResult> GetUsers(
        [FromQuery] int Page, [FromQuery] int PageSize,
        CancellationToken cancellationToken)
    {
        var users = await _mediator.Send(new GetUsersCommand(Page, PageSize), cancellationToken);
        return Results.Ok(users);
    }

    [HttpGet("get-current-user")]
    public async Task<IResult> GetCurrentUser(CancellationToken cancellatioToken)
    {
        var user = await _mediator.Send(new GetCurrentUserQuery(), cancellatioToken);

        return user != null ? Results.Ok() : Results.NotFound("user is not found");
    }

    // [HttpPut("update-profile")]
    // [Authorize]
    // public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    // {
    //     var currentUser = _httpContext.HttpContext?.User;
    //     if (currentUser == null)
    //         throw new ArgumentNullException("Пользователь не найден, нужно войти в профиль или зарегистрироваться");

    //     var userId = currentUser?.FindFirst("userId")!.Value;
    //     if (userId == null || !Guid.TryParse(userId, out var userGuid))
    //         return Unauthorized("user or userId not found");

    //     await _usersService.UpdateProfile(userGuid, request.NewUserName, request.NewEmail, request.Role);
    //     return Ok();
    // }
}
