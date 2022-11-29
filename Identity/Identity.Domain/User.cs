using System.Text.Json.Serialization;
using RaiffeisenClone.Domain;

namespace Identity.Domain;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    [JsonIgnore] 
    public List<RefreshToken> RefreshTokens { get; set; } = new();
    
    [JsonIgnore]
    public virtual Avatar Avatar { get; set; }
    
    public Guid? AvatarId { get; set; }
}