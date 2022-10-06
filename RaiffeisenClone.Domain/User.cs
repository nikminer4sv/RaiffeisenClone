using System.Text.Json.Serialization;

namespace RaiffeisenClone.Domain;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public string Username { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    [JsonIgnore] public List<RefreshToken> RefreshTokens { get; set; } = new();
}