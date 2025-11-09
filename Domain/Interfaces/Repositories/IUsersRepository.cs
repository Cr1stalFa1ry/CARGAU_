using Domain.Models.User;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
}
