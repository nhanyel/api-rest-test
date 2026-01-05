namespace Application.Users.Register;

public class RegisterUserCommand
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public RegisterUserCommand(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}