using MediatR;
using Domain.Models.User;

namespace Application.Users.Commands.CreateNewUser;

public record class CreateUserCommand(
    string UserName,
    string Email,
    string Password,
    string PhoneNumber,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    int Role) : IRequest<(string, string)>;