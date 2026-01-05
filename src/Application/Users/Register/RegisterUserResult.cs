using System;

namespace Application.Users.Register;

public class RegisterUserResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}