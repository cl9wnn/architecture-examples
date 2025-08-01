namespace Infrastructure.Database.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public RefreshTokenEntity RefreshToken { get; set; } 
}