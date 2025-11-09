using MediatR;
using Domain.Models.User;

namespace Application.Users.Commands.RegisterUser;

public record class RegisterUserCommand(
    string UserName,
    string Email,
    string Password,
    string PhoneNumber,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    int Role) : IRequest<(string, string)>;