namespace Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    private User() { }

    public User(Guid id, string name, string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty", nameof(email));

        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }
}