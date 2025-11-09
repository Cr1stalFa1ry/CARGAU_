namespace Domain.Interfaces.Users.Services;

public interface IUserContextService
{
    Guid? GetCurrentUserId();
    string GetCurrentUserEmail();
}
