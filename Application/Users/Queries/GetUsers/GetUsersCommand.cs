using Domain.Models.User;
using Domain.ResponseModels.User;
using MediatR;

namespace Application.Users.Queries.GetUsers;

public record class GetUsersCommand(int Page, int PageSize) : IRequest<List<UserResponse>>;
