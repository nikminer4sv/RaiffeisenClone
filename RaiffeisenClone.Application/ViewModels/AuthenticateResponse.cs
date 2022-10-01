using System.Text.Json.Serialization;
using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Application.ViewModels;

public class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    
    public DateTime DayOfBirth { get; set; }
    public string JwtToken { get; set; }

    [JsonIgnore] // refresh token is returned in http only cookie
    public string RefreshToken { get; set; }

    public AuthenticateResponse(User user, string jwtToken, string refreshToken)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        DayOfBirth = user.DateOfBirth;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }
}