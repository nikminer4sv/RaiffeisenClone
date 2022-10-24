using Microsoft.AspNetCore.Http;

namespace RaiffeisenClone.Application.Interfaces;

public interface IAvatarService
{
    Task<Guid> UploadAsync(Guid userId, IFormFile file);
}