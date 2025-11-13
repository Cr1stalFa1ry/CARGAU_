namespace Domain.ResponseModels.User;

public record UserResponse
{
    /// <summary>
    /// Модель ответа, представяющая основную информацию для админа
    /// </summary>
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
