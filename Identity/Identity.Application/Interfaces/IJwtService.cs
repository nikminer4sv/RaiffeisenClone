using Identity.Domain;
using RaiffeisenClone.Domain;

namespace Identity.Application.Interfaces;

public interface IJwtService : IService
{
    string GenerateJwtToken(User user);

    Guid? ValidateJwtToken(string? token);

    RefreshToken GenerateRefreshToken(string ipAddress);
}