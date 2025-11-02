using System;

namespace Domain.Entities;

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public List<UserEntity> Users { get; set; } = []; // оставлю по итогу, чтобы потом можно было по роли вытаскивать пользователей
}
