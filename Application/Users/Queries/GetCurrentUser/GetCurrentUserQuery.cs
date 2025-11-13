using Domain.ResponseModels.User;
using MediatR;

namespace Application.Users.Queries.GetCurrentUser;

public record class GetCurrentUserQuery() : IRequest<UserResponse>;
