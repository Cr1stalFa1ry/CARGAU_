using System;

namespace Domain.Interfaces.Users.PasswordHasher;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string hashedPassword);
}
