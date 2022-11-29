using Identity.Application.ViewModels;
using Identity.Application.ViewModels.LoginViewModel;
using Identity.Application.ViewModels.RegisterViewModel;
using RaiffeisenClone.Application.ViewModels;

namespace Identity.Application.Interfaces;

public interface IAuthService : IService
{
    Task<AuthenticateResponse> Authenticate(LoginViewModel model, string ipAddress);

    Task<RegisterResponse> Register(RegisterViewModel registerViewModel);

    Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);

    Task RevokeToken(string token, string ipAddress);
}