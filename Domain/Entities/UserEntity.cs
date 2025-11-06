using System;

namespace Domain.Entities;

public class UserEntity
{
    /// <summary>
    /// Пользовательские данные
    /// </summary>
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Контактная информация
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Связные сущности с ключами
    /// </summary>
    public List<OrderEntity> Orders { get; set; } = [];
    public List<CarEntity> Cars { get; set; } = [];
    public List<RefreshTokenEntity> RefreshTokens { get; set; } = [];
    public RoleEntity? Role { get; set; }
    public int? RoleId { get; set; }
}