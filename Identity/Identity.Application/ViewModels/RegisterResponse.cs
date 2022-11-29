using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Application.ViewModels;

public class RegisterResponse
{
    public string Username { get; set; }
    public string Password { get; set; }

    public RegisterResponse(string username, string password)
    {
        Username = username;
        Password = password;
    }
}