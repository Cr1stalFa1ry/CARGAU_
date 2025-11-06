using System;

namespace Domain.Models.User;

public class User
{
    /// <summary>
    /// Конструктор пользователя при создании jwt 
    /// </summary>
    public User(Guid id, string userName, string email, Roles role)
    {
        Id = id;
        UserName = userName;
        Email = email;
        Role = role;
    }

    /// <summary>
    /// Конструктор пользователя маппинге с сущности
    /// </summary>
    public User(Guid id, string userName,
                string email, Roles role,
                string phoneNumber, string firstName,
                string lastName, DateOnly dateOfBirth)
    {
        Id = id;
        UserName = userName;
        Email = email;
        Role = role;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }


    /// <summary>
    /// Пользовательские данные
    /// </summary>
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    //public string Password { get; set; } = string.Empty;
    public Roles Role { get; set; }

    /// <summary>
    /// Контактная информация
    /// </summary>
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Метод для создания объекта пользователя при создании jwt 
    /// </summary>
    public static User Create(Guid id, string userName, string email, Roles role)
    {
        return new User(id, userName, email, role);
    }
    
    /// <summary>
    /// Метод для создания пользователя при маппинге с сущности
    /// </summary>
    public static User Create(
                Guid id, string userName,
                string email, Roles role,
                string phoneNumber, string firstName,
                string lastName, DateOnly dateOfBirth)
    {
        return new User(id, userName, email, role, phoneNumber, firstName, lastName, dateOfBirth);
    }
}
