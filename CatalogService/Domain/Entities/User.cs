namespace ProductService.Domain.Entities;

public class User
{
    public Guid Id { get; init; }
    public string Username { get; set; }
    public string HashPassword { get; set; }
    public string Email { get; set; }

    public User(string userName, string hashPassword, string email)
    {
        Id = Guid.NewGuid();
        Username = userName;
        HashPassword = hashPassword;
        Email = email;
    }
}