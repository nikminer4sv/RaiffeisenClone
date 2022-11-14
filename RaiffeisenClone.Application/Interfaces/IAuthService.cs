using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Application.Interfaces;

public interface IAuthService : IService
{
    Task<AuthenticateResponse> Authenticate(LoginViewModel model, string ipAddress);

    Task<RegisterResponse> Register(RegisterViewModel registerViewModel);

    Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);

    Task RevokeToken(string token, string ipAddress);
}