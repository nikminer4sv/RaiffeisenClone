using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Application.Interfaces;

public interface IJwtService : IService
{
    string GenerateJwtToken(User user);

    Guid? ValidateJwtToken(string? token);

    RefreshToken GenerateRefreshToken(string ipAddress);
}